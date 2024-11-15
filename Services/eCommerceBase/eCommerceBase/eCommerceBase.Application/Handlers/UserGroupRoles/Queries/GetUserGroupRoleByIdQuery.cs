using MediatR;
using eCommerceBase.Domain.Result;
using eCommerceBase.Domain.AggregateModels;
using eCommerceBase.Persistence.GenericRepository;
using eCommerceBase.Insfrastructure.Utilities.Caching.Redis;

namespace eCommerceBase.Application.Handlers.UserGroupRoles.Queries;
public record GetUserGroupRoleByIdQuery(System.Guid Id) : IRequest<Result<UserGroupRole>>;
public class GetUserGroupRoleByIdQueryHandler(IReadDbRepository<UserGroupRole> userGroupRoleRepository,
		ICacheService cacheService) : IRequestHandler<GetUserGroupRoleByIdQuery,
		Result<UserGroupRole>>
{
    private readonly IReadDbRepository<UserGroupRole> _userGroupRoleRepository = userGroupRoleRepository;
    private readonly ICacheService _cacheService = cacheService;
    public async Task<Result<UserGroupRole>> Handle(GetUserGroupRoleByIdQuery request,
		CancellationToken cancellationToken)
    {
        return await _cacheService.GetAsync(request,
		async () =>
        {
            var query = await _userGroupRoleRepository.GetByIdAsync(request.Id);
            return Result.SuccessDataResult<UserGroupRole>(query!);
        },
		cancellationToken);
    }
}