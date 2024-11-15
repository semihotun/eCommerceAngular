using MediatR;
using eCommerceBase.Domain.AggregateModels;
using eCommerceBase.Insfrastructure.Utilities.Grid.PagedList;
using eCommerceBase.Domain.Result;
using eCommerceBase.Application.Handlers.UserGroups.Queries.Dtos;
using eCommerceBase.Insfrastructure.Utilities.Caching.Redis;
using eCommerceBase.Persistence.Context;
using eCommerceBase.Insfrastructure.Utilities.Grid.Filter;

namespace eCommerceBase.Application.Handlers.UserGroups.Queries;
public record GetUserGroupGridDTOQuery(int PageIndex, int PageSize, string? OrderByColumnName, List<FilterModel>? FilterModelList) : IRequest<Result<PagedList<UserGroupGridDTO>>>;
public class GetUserGroupGridDTOQueryHandler(CoreDbContext coreDbContext, ICacheService cacheService) : IRequestHandler<GetUserGroupGridDTOQuery, Result<PagedList<UserGroupGridDTO>>>
{
    private readonly CoreDbContext _coreDbContext = coreDbContext;
    private readonly ICacheService _cacheService = cacheService;
    public async Task<Result<PagedList<UserGroupGridDTO>>> Handle(GetUserGroupGridDTOQuery request, CancellationToken cancellationToken)
    {
        return await _cacheService.GetAsync<Result<PagedList<UserGroupGridDTO>>>(request, async () =>
        {
            var query = await _coreDbContext.Query<UserGroup>().Select(x => new UserGroupGridDTO { Id = x.Id, Name = x.Name }).ToTableSettings(new PagedListFilterModel() { PageIndex = request.PageIndex, PageSize = request.PageSize, FilterModelList = request.FilterModelList, OrderByColumnName = request.OrderByColumnName });
            return Result.SuccessDataResult<PagedList<UserGroupGridDTO>>(query);
        }, cancellationToken);
    }
}