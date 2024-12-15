using eCommerceBase.Application.Constants;
using eCommerceBase.Application.IntegrationEvents.CustomerRegister;
using eCommerceBase.Domain.AggregateModels;
using eCommerceBase.Domain.Constant;
using eCommerceBase.Domain.Result;
using eCommerceBase.Insfrastructure.Utilities.Identity.Service;
using eCommerceBase.Insfrastructure.Utilities.Security.Hashing;
using eCommerceBase.Insfrastructure.Utilities.Security.Jwt;
using eCommerceBase.Persistence.GenericRepository;
using eCommerceBase.Persistence.UnitOfWork;
using MediatR;
namespace eCommerceBase.Application.Handlers.CustomerUsers.Commands
{
    public record CustomerUserRegisterCommand(string Email,
        string Password,
        string FirstName,
        string LastName) : IRequest<Result<AccessToken>>;

    public class CustomerUserRegisterCommandHandler(
        IWriteDbRepository<CustomerUser> customerUserRepository,
        IUnitOfWork unitOfWork,
        IReadDbRepository<UserGroupRole> userGroupRoleRepository,
        ITokenService tokenService) : IRequestHandler<CustomerUserRegisterCommand, Result<AccessToken>>
    {
        private readonly IWriteDbRepository<CustomerUser> _customerUserRepository = customerUserRepository;
        private readonly IReadDbRepository<UserGroupRole> _userGroupRoleRepository = userGroupRoleRepository;
        private readonly IUnitOfWork _unitOfWork = unitOfWork;
        private readonly ITokenService _tokenService = tokenService;
        public async Task<Result<AccessToken>> Handle(CustomerUserRegisterCommand request, CancellationToken cancellationToken)
        {
            return await _unitOfWork.BeginTransactionAndCreateOutbox<Result<AccessToken>>(async (successAction) =>
            {
                if (await _customerUserRepository.AnyAsync(x => x.Email == request.Email))
                    return Result.ErrorDataResult<AccessToken>(Messages.EmailAlreadyExist);

                HashingHelper.CreatePasswordHash(request.Password, out byte[] passwordHash, out byte[] passwordSalt);
                var user = new CustomerUser(request.FirstName,
                    request.LastName,
                    request.Email,
                    passwordSalt,
                    passwordHash,
                    true,
                    false);
                user.SetUserForUserGroup();
                await _customerUserRepository.AddAsync(user);

                var userGroub = (await _userGroupRoleRepository.ToListAsync(x => x.UserGroupId == Guid.Parse(InitConst.UserGuid),
                    x => x.Role!)).Select(x => x.Role!.RoleName);

                var result = _tokenService.CreateToken(user, userGroub);

                var outBoxMessage = new CustomerMailActivationSendedStartedIE(user.Id,request.Email,
                    request.FirstName, request.LastName);
                successAction(outBoxMessage);

                return Result.SuccessDataResult(result);
            });
        }
    }
}
