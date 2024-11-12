using MediatR;
using eCommerceBase.Domain.Result;
using eCommerceBase.Domain.AggregateModels;
using eCommerceBase.Persistence.GenericRepository;
using eCommerceBase.Insfrastructure.Utilities.Caching.Redis;

namespace eCommerceBase.Application.Handlers.Warehouses.Queries;
public record GetAllWarehouse() : IRequest<Result<IList<Warehouse>>>;
public class GetAllWarehouseHandler(IReadDbRepository<Warehouse> warehouseRepository,
		ICacheService cacheService) : IRequestHandler<GetAllWarehouse,
		Result<IList<Warehouse>>>
{
    private readonly IReadDbRepository<Warehouse> _warehouseRepository = warehouseRepository;
    private readonly ICacheService _cacheService = cacheService;
    public async Task<Result<IList<Warehouse>>> Handle(GetAllWarehouse request,
		CancellationToken cancellationToken)
    {
        return await _cacheService.GetAsync(request,
		async () =>
        {
            var data = await _warehouseRepository.ToListAsync();
            return Result.SuccessDataResult(data!);
        },
		cancellationToken);
    }
}