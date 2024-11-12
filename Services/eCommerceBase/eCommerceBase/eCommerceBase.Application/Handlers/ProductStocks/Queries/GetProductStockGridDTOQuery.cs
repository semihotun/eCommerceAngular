using MediatR;
using eCommerceBase.Domain.AggregateModels;
using eCommerceBase.Insfrastructure.Utilities.Grid.PagedList;
using eCommerceBase.Domain.Result;
using eCommerceBase.Application.Handlers.ProductStocks.Queries.Dtos;
using eCommerceBase.Insfrastructure.Utilities.Caching.Redis;
using eCommerceBase.Persistence.Context;
using eCommerceBase.Insfrastructure.Utilities.Grid.Filter;
using Microsoft.EntityFrameworkCore;

namespace eCommerceBase.Application.Handlers.ProductStocks.Queries;
public record GetProductStockGridDTOQuery(Guid ProductId, int PageIndex, int PageSize, string? OrderByColumnName, List<FilterModel>? FilterModelList)
    : IRequest<Result<PagedList<ProductStockGridDTO>>>;
public class GetProductStockGridDTOQueryHandler(CoreDbContext coreDbContext, ICacheService cacheService)
    : IRequestHandler<GetProductStockGridDTOQuery, Result<PagedList<ProductStockGridDTO>>>
{
    private readonly CoreDbContext _coreDbContext = coreDbContext;
    private readonly ICacheService _cacheService = cacheService;
    public async Task<Result<PagedList<ProductStockGridDTO>>> Handle(GetProductStockGridDTOQuery request, CancellationToken cancellationToken)
    {
        return await _cacheService.GetAsync<Result<PagedList<ProductStockGridDTO>>>(request, async () =>
        {
            var query = await _coreDbContext.Query<ProductStock>()
            .Include(x => x.Warehouse)
            .Where(x => x.ProductId == request.ProductId)
            .Select(x => new ProductStockGridDTO
            {
                Id = x.Id,
                RemainingStock = x.RemainingStock,
                TotalStock = x.TotalStock,
                WarehouseName = x.Warehouse!.Name
            }).ToTableSettings(new PagedListFilterModel()
            {
                PageIndex = request.PageIndex,
                PageSize = request.PageSize,
                FilterModelList = request.FilterModelList,
                OrderByColumnName = request.OrderByColumnName
            });
            return Result.SuccessDataResult<PagedList<ProductStockGridDTO>>(query);
        }, cancellationToken);
    }
}