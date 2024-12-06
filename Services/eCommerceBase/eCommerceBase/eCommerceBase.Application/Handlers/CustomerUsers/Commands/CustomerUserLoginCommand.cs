using eCommerceBase.Application.Constants;
using eCommerceBase.Domain.AggregateModels;
using eCommerceBase.Domain.Result;
using eCommerceBase.Insfrastructure.Utilities.Identity.Service;
using eCommerceBase.Insfrastructure.Utilities.Security.Hashing;
using eCommerceBase.Insfrastructure.Utilities.Security.Jwt;
using eCommerceBase.Persistence.GenericRepository;
using MediatR;

namespace eCommerceBase.Application.Handlers.CustomerUsers.Commands
{
    public record CustomerUserLoginCommand(string Email,
    string Password) : IRequest<Result<AccessToken>>;

    public class CustomerUserLoginCommandHandler(
        IWriteDbRepository<CustomerUser> customerUserRepository,
        IReadDbRepository<UserGroupRole> userGroupRoleRepository,
        ITokenService tokenService) : IRequestHandler<CustomerUserLoginCommand, Result<AccessToken>>
    {
        private readonly IWriteDbRepository<CustomerUser> _customerUserRepository = customerUserRepository;
        private readonly IReadDbRepository<UserGroupRole> _userGroupRoleRepository = userGroupRoleRepository;
        private readonly ITokenService _tokenService = tokenService;
        public async Task<Result<AccessToken>> Handle(CustomerUserLoginCommand request, CancellationToken cancellationToken)
        {
            var userToCheck = await _customerUserRepository.GetAsync(x => x.Email == request.Email);
            if (userToCheck == null)
            {
                return Result.ErrorDataResult<AccessToken>(Messages.UserNotFound);
            }
            if (!HashingHelper.VerifyPasswordHash(request.Password, userToCheck.PasswordHash, userToCheck.PasswordSalt))
            {
                return Result.ErrorDataResult<AccessToken>(Messages.PasswordError);
            }
            var userGroub = (await _userGroupRoleRepository.ToListAsync(x => x.UserGroupId == userToCheck.UserGroupId,
                       x => x.Role!)).Select(x => x.Role!.RoleName);

            var result = _tokenService.CreateToken(userToCheck, userGroub);

            return Result.SuccessDataResult(result, Messages.OperationSuccess);
        }
    }
}
