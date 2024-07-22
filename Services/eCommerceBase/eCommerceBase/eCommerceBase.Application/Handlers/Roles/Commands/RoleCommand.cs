using eCommerceBase.Domain.AggregateModels;
using eCommerceBase.Domain.Constant;
using eCommerceBase.Domain.Result;
using eCommerceBase.Insfrastructure.Utilities.Caching.Redis;
using eCommerceBase.Persistence.GenericRepository;
using eCommerceBase.Persistence.UnitOfWork;
using MediatR;

namespace eCommerceBase.Application.Handlers.Roles.Commands
{
    public record RoleCommand(string[] RolePath) : IRequest<Result>;

    public class RoleCommandandler(IWriteDbRepository<Role> roleRepository,
        IWriteDbRepository<UserGroupRole> userGroupRoleRepository,
        IUnitOfWork unitOfWork,
        ICacheService cacheService) : IRequestHandler<RoleCommand, Result>
    {
        private readonly IWriteDbRepository<Role> _roleRepository = roleRepository;
        private readonly IWriteDbRepository<UserGroupRole> _userGroupRoleRepository = userGroupRoleRepository;
        private readonly IUnitOfWork _unitOfWork = unitOfWork;
        private readonly ICacheService _cacheService = cacheService;

        public async Task<Result> Handle(RoleCommand request, CancellationToken cancellationToken)
        {
            var newRole = new List<Role>();
            await _unitOfWork.BeginTransaction<Result>(async () =>
            {
                newRole = request.RolePath
                           .Where(newAdminRole => !_roleRepository.AnyAsync(existingAdminRole => existingAdminRole.RoleName == newAdminRole).Result)
                           .Select(x => new Role(x)).ToList();
                if (newRole != null)
                {
                    await _roleRepository.AddRangeAsync(newRole);
                }       
                await _cacheService.RemovePatternAsync("eCommerceBase:Roles");
                return Result.SuccessResult();
            });
            //Şimdilik 2 unit of work sonrasında farklı handler'a çekilicek
            return await _unitOfWork.BeginTransaction<Result>(async () =>
            {
                foreach (var item in newRole!)
                {
                    var userGroupRole = new UserGroupRole(Guid.Parse(InitConst.AdminGuid), item.Id);
                    await _userGroupRoleRepository.AddAsync(userGroupRole);
                }
                await _cacheService.RemovePatternAsync("eCommerceBase:UserGroupRoles");
                return Result.SuccessResult();
            });
        }
    }
}
