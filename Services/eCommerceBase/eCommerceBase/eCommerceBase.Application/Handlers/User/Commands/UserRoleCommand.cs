using eCommerceBase.Domain.AggregateModels;
using eCommerceBase.Domain.Result;
using eCommerceBase.Insfrastructure.Utilities.Caching.Redis;
using eCommerceBase.Persistence.GenericRepository;
using eCommerceBase.Persistence.UnitOfWork;
using MediatR;

namespace eCommerceBase.Application.Handlers.User.Commands
{
    public record UserRoleCommand(string[] RolePath) : IRequest<Result>;

    public class UserRoleCommandandler(IWriteDbRepository<Role> roleRepository,
        IUnitOfWork unitOfWork,
        ICacheService cacheService) : IRequestHandler<UserRoleCommand, Result>
    {
        private readonly IWriteDbRepository<Role> _roleRepository = roleRepository;
        private readonly IUnitOfWork _unitOfWork = unitOfWork;
        private readonly ICacheService _cacheService = cacheService;

        public async Task<Result> Handle(UserRoleCommand request, CancellationToken cancellationToken)
        {
            return await _unitOfWork.BeginTransaction<Result>(async () =>
            {
                var newProducts = request.RolePath
                           .Where(newAdminRole => !_roleRepository.AnyAsync(existingAdminRole => existingAdminRole.RoleName == newAdminRole).Result)
                           .Select(x => new Role(x));
                if (newProducts != null)
                {
                    await _roleRepository.AddRangeAsync(newProducts);
                }
                await _cacheService.RemovePatternAsync("AdminIdentityService:AdminRoles");
                return Result.SuccessResult();
            });
        }
    }
}
