using MediatR;
using eCommerceBase.Domain.Result;
using eCommerceBase.Domain.AggregateModels;
using eCommerceBase.Persistence.UnitOfWork;
using eCommerceBase.Persistence.GenericRepository;
using eCommerceBase.Insfrastructure.Utilities.Caching.Redis;
using eCommerceBase.Application.Constants;
using eCommerceBase.Application.Handlers.Mapper;

namespace eCommerceBase.Application.Handlers.Warehouses.Commands;
public record CreateWarehouseCommand(string Name,
		string Address) : IRequest<Result>;
public class CreateWarehouseCommandHandler(IWriteDbRepository<Warehouse> warehouseRepository,
		IUnitOfWork unitOfWork,
		ICacheService cacheService) : IRequestHandler<CreateWarehouseCommand,
		Result>
{
    private readonly IWriteDbRepository<Warehouse> _warehouseRepository = warehouseRepository;
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    private readonly ICacheService _cacheService = cacheService;
    public async Task<Result> Handle(CreateWarehouseCommand request,
		CancellationToken cancellationToken)
    {
        return await _unitOfWork.BeginTransaction(async () =>
        {
            var data = WarehouseMapper.CreateWarehouseCommandToWarehouse(request);
            await _warehouseRepository.AddAsync(data);
            await _cacheService.RemovePatternAsync("eCommerceBase:Warehouses");
            return Result.SuccessResult(Messages.Added);
        });
    }
}