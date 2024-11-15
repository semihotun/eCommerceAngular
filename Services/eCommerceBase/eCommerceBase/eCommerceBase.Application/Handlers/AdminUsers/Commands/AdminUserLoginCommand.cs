using eCommerceBase.Application.Constants;
using eCommerceBase.Domain.AggregateModels;
using eCommerceBase.Domain.Result;
using eCommerceBase.Insfrastructure.Utilities.Identity.Service;
using eCommerceBase.Insfrastructure.Utilities.Security.Hashing;
using eCommerceBase.Insfrastructure.Utilities.Security.Jwt;
using eCommerceBase.Persistence.GenericRepository;
using MediatR;

namespace eCommerceBase.Application.Handlers.AdminUsers.Commands
{
    public record AdminUserLoginCommand(string Email,
    string Password) : IRequest<Result<AccessToken>>;

    public class AdminUserLoginCommandHandler(
        IWriteDbRepository<AdminUser> adminUserRepository,
        IReadDbRepository<UserGroupRole> userGroupRoleRepository,
        ITokenService tokenService) : IRequestHandler<AdminUserLoginCommand, Result<AccessToken>>
    {
        private readonly IWriteDbRepository<AdminUser> _adminUserRepository = adminUserRepository;
        private readonly IReadDbRepository<UserGroupRole> _userGroupRoleRepository = userGroupRoleRepository;
        private readonly ITokenService _tokenService = tokenService;
        public async Task<Result<AccessToken>> Handle(AdminUserLoginCommand request, CancellationToken cancellationToken)
        {
            var userToCheck = await _adminUserRepository.GetAsync(x => x.Email == request.Email);
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
