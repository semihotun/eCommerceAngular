using eCommerceBase.Application.Handlers.ProductStocks.Queries.Dtos;
using eCommerceBase.Domain.AggregateModels;
using eCommerceBase.Domain.Result;
using eCommerceBase.Insfrastructure.Utilities.Caching.Redis;
using eCommerceBase.Insfrastructure.Utilities.Grid.Filter;
using eCommerceBase.Insfrastructure.Utilities.Grid.PagedList;
using eCommerceBase.Persistence.GenericRepository;
using MediatR;

namespace eCommerceBase.Application.Handlers.ProductStocks.Queries;
public record GetProductStockGridDTOQuery(Guid ProductId, int PageIndex, int PageSize, string? OrderByColumnName, List<FilterModel>? FilterModelList)
    : IRequest<Result<PagedList<ProductStockGridDTO>>>;
public class GetProductStockGridDTOQueryHandler(IReadDbRepository<ProductStock> productStockRepository, ICacheService cacheService)
    : IRequestHandler<GetProductStockGridDTOQuery, Result<PagedList<ProductStockGridDTO>>>
{
    private readonly IReadDbRepository<ProductStock> _productStockRepository = productStockRepository;
    private readonly ICacheService _cacheService = cacheService;
    public async Task<Result<PagedList<ProductStockGridDTO>>> Handle(GetProductStockGridDTOQuery request, CancellationToken cancellationToken)
    {
        return await _cacheService.GetAsync(request, async () =>
        {
            var query = await _productStockRepository.Query()
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
            return Result.SuccessDataResult(query);
        }, cancellationToken);
    }
}