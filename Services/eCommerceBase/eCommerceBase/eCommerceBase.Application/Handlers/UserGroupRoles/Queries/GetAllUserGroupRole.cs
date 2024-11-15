using MediatR;
using eCommerceBase.Domain.Result;
using eCommerceBase.Domain.AggregateModels;
using eCommerceBase.Persistence.GenericRepository;
using eCommerceBase.Insfrastructure.Utilities.Caching.Redis;

namespace eCommerceBase.Application.Handlers.UserGroupRoles.Queries;
public record GetAllUserGroupRole() : IRequest<Result<IList<UserGroupRole>>>;
public class GetAllUserGroupRoleHandler(IReadDbRepository<UserGroupRole> userGroupRoleRepository,
		ICacheService cacheService) : IRequestHandler<GetAllUserGroupRole,
		Result<IList<UserGroupRole>>>
{
    private readonly IReadDbRepository<UserGroupRole> _userGroupRoleRepository = userGroupRoleRepository;
    private readonly ICacheService _cacheService = cacheService;
    public async Task<Result<IList<UserGroupRole>>> Handle(GetAllUserGroupRole request,
		CancellationToken cancellationToken)
    {
        return await _cacheService.GetAsync(request,
		async () =>
        {
            var data = await _userGroupRoleRepository.ToListAsync();
            return Result.SuccessDataResult(data!);
        },
		cancellationToken);
    }
}