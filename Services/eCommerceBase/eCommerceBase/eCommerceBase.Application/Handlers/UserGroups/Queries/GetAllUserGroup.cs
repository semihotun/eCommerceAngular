using MediatR;
using eCommerceBase.Domain.Result;
using eCommerceBase.Domain.AggregateModels;
using eCommerceBase.Persistence.GenericRepository;
using eCommerceBase.Insfrastructure.Utilities.Caching.Redis;

namespace eCommerceBase.Application.Handlers.UserGroups.Queries;
public record GetAllUserGroup() : IRequest<Result<IList<UserGroup>>>;
public class GetAllUserGroupHandler(IReadDbRepository<UserGroup> userGroupRepository,
		ICacheService cacheService) : IRequestHandler<GetAllUserGroup,
		Result<IList<UserGroup>>>
{
    private readonly IReadDbRepository<UserGroup> _userGroupRepository = userGroupRepository;
    private readonly ICacheService _cacheService = cacheService;
    public async Task<Result<IList<UserGroup>>> Handle(GetAllUserGroup request,
		CancellationToken cancellationToken)
    {
        return await _cacheService.GetAsync(request,
		async () =>
        {
            var data = await _userGroupRepository.ToListAsync(x=>x.IsEditable);
            return Result.SuccessDataResult(data!);
        },
		cancellationToken);
    }
}