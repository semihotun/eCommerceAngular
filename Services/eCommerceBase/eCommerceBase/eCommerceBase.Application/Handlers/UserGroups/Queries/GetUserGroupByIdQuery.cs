using MediatR;
using eCommerceBase.Domain.Result;
using eCommerceBase.Domain.AggregateModels;
using eCommerceBase.Persistence.GenericRepository;
using eCommerceBase.Insfrastructure.Utilities.Caching.Redis;

namespace eCommerceBase.Application.Handlers.UserGroups.Queries;
public record GetUserGroupByIdQuery(System.Guid Id) : IRequest<Result<UserGroup>>;
public class GetUserGroupByIdQueryHandler(IReadDbRepository<UserGroup> userGroupRepository,
		ICacheService cacheService) : IRequestHandler<GetUserGroupByIdQuery,
		Result<UserGroup>>
{
    private readonly IReadDbRepository<UserGroup> _userGroupRepository = userGroupRepository;
    private readonly ICacheService _cacheService = cacheService;
    public async Task<Result<UserGroup>> Handle(GetUserGroupByIdQuery request,
		CancellationToken cancellationToken)
    {
        return await _cacheService.GetAsync(request,
		async () =>
        {
            var query = await _userGroupRepository.GetByIdAsync(request.Id);
            return Result.SuccessDataResult<UserGroup>(query!);
        },
		cancellationToken);
    }
}