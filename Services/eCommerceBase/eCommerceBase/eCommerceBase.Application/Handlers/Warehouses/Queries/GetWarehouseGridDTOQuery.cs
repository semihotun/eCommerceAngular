using eCommerceBase.Application.Handlers.Warehouses.Queries.Dtos;
using eCommerceBase.Domain.AggregateModels;
using eCommerceBase.Domain.Result;
using eCommerceBase.Insfrastructure.Utilities.Caching.Redis;
using eCommerceBase.Insfrastructure.Utilities.Grid.Filter;
using eCommerceBase.Insfrastructure.Utilities.Grid.PagedList;
using eCommerceBase.Persistence.GenericRepository;
using MediatR;

namespace eCommerceBase.Application.Handlers.Warehouses.Queries;
public record GetWarehouseGridDTOQuery(int PageIndex, int PageSize, string? OrderByColumnName, List<FilterModel>? FilterModelList)
    : IRequest<Result<PagedList<WarehouseGridDTO>>>;
public class GetWarehouseGridDTOQueryHandler(IReadDbRepository<Warehouse> warehouseRepository, ICacheService cacheService) 
    : IRequestHandler<GetWarehouseGridDTOQuery, Result<PagedList<WarehouseGridDTO>>>
{
    private readonly IReadDbRepository<Warehouse> _warehouseRepository = warehouseRepository;
    private readonly ICacheService _cacheService = cacheService;
    public async Task<Result<PagedList<WarehouseGridDTO>>> Handle(GetWarehouseGridDTOQuery request, CancellationToken cancellationToken)
    {
        return await _cacheService.GetAsync(request, async () =>
        {
            var query = await _warehouseRepository.Query().Select(x => new WarehouseGridDTO
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
            return Result.SuccessDataResult(query);
        }, cancellationToken);
    }
}