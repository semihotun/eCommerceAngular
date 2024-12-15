using MediatR;
using eCommerceBase.Domain.Result;
using eCommerceBase.Domain.AggregateModels;
using eCommerceBase.Persistence.GenericRepository;
using eCommerceBase.Insfrastructure.Utilities.Caching.Redis;

namespace eCommerceBase.Application.Handlers.Warehouses.Queries;
public record GetWarehouseByIdQuery(System.Guid Id) : IRequest<Result<Warehouse>>;
public class GetWarehouseByIdQueryHandler(IReadDbRepository<Warehouse> warehouseRepository,
		ICacheService cacheService) : IRequestHandler<GetWarehouseByIdQuery,
		Result<Warehouse>>
{
    private readonly IReadDbRepository<Warehouse> _warehouseRepository = warehouseRepository;
    private readonly ICacheService _cacheService = cacheService;
    public async Task<Result<Warehouse>> Handle(GetWarehouseByIdQuery request,
		CancellationToken cancellationToken)
    {
        return await _cacheService.GetAsync(request,
		async () =>
        {
            var query = await _warehouseRepository.GetByIdAsync(request.Id);
            return Result.SuccessDataResult(query!);
        },
		cancellationToken);
    }
}