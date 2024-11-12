using MediatR;
using eCommerceBase.Domain.AggregateModels;
using eCommerceBase.Insfrastructure.Utilities.Grid.PagedList;
using eCommerceBase.Domain.Result;
using eCommerceBase.Application.Handlers.Warehouses.Queries.Dtos;
using eCommerceBase.Insfrastructure.Utilities.Caching.Redis;
using eCommerceBase.Persistence.Context;
using eCommerceBase.Insfrastructure.Utilities.Grid.Filter;

namespace eCommerceBase.Application.Handlers.Warehouses.Queries;
public record GetWarehouseGridDTOQuery(int PageIndex, int PageSize, string? OrderByColumnName, List<FilterModel>? FilterModelList)
    : IRequest<Result<PagedList<WarehouseGridDTO>>>;
public class GetWarehouseGridDTOQueryHandler(CoreDbContext coreDbContext, ICacheService cacheService) 
    : IRequestHandler<GetWarehouseGridDTOQuery, Result<PagedList<WarehouseGridDTO>>>
{
    private readonly CoreDbContext _coreDbContext = coreDbContext;
    private readonly ICacheService _cacheService = cacheService;
    public async Task<Result<PagedList<WarehouseGridDTO>>> Handle(GetWarehouseGridDTOQuery request, CancellationToken cancellationToken)
    {
        return await _cacheService.GetAsync<Result<PagedList<WarehouseGridDTO>>>(request, async () =>
        {
            var query = await _coreDbContext.Query<Warehouse>().Select(x => new WarehouseGridDTO
            {
                Id = x.Id,
                Name = x.Name,
                Address = x.Address
            }).ToTableSettings(new PagedListFilterModel()
            {
                PageIndex = request.PageIndex,
                PageSize = request.PageSize,
                FilterModelList = request.FilterModelList,
                OrderByColumnName = request.OrderByColumnName
            });
            return Result.SuccessDataResult<PagedList<WarehouseGridDTO>>(query);
        }, cancellationToken);
    }
}