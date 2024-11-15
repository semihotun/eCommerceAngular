using eCommerceBase.Application.Constants;
using eCommerceBase.Domain.AggregateModels;
using eCommerceBase.Domain.Constant;
using eCommerceBase.Domain.Result;
using eCommerceBase.Insfrastructure.Utilities.Identity.Service;
using eCommerceBase.Insfrastructure.Utilities.Security.Hashing;
using eCommerceBase.Insfrastructure.Utilities.Security.Jwt;
using eCommerceBase.Persistence.GenericRepository;
using eCommerceBase.Persistence.UnitOfWork;
using MediatR;
namespace eCommerceBase.Application.Handlers.AdminUsers.Commands
{
    public record AdminUserRegisterCommand(string Email,
        string Password,
        string FirstName,
        string LastName) : IRequest<Result<AccessToken>>;

    public class AdminUserRegisterCommanHandler(
        IWriteDbRepository<AdminUser> adminUserRepository,
        IUnitOfWork unitOfWork,
        IReadDbRepository<UserGroupRole> userGroupRoleRepository,
        ITokenService tokenService) : IRequestHandler<AdminUserRegisterCommand, Result<AccessToken>>
    {
        private readonly IWriteDbRepository<AdminUser> _adminUserRepository = adminUserRepository;
        private readonly IReadDbRepository<UserGroupRole> _userGroupRoleRepository = userGroupRoleRepository;
        private readonly IUnitOfWork _unitOfWork = unitOfWork;
        private readonly ITokenService _tokenService = tokenService;
        public async Task<Result<AccessToken>> Handle(AdminUserRegisterCommand request, CancellationToken cancellationToken)
        {
            return await _unitOfWork.BeginTransaction(async () =>
            {
                if (await _adminUserRepository.GetCountAsync() == 0)
                {
                    HashingHelper.CreatePasswordHash(request.Password, out byte[] passwordHash, out byte[] passwordSalt);
                    var user = new AdminUser(request.FirstName,
                        request.LastName,
                        request.Email,
                        passwordSalt,
                        passwordHash,
                        true);
                    user.SetAdminForUserGroup();                                        
                    await _adminUserRepository.AddAsync(user);

                    var userGroub = (await _userGroupRoleRepository.ToListAsync(x => x.UserGroupId == Guid.Parse(InitConst.AdminGuid),
                        x => x.Role!)).Select(x=>x.Role!.RoleName);

                    var result = _tokenService.CreateToken(user,userGroub);
                    return Result.SuccessDataResult(result);
                }
                else
                {
                    return Result.ErrorDataResult<AccessToken>(Messages.NameAlreadyExist);
                }
            });
        }
    }
}
