using MediatR;
using eCommerceBase.Domain.Result;
using eCommerceBase.Domain.AggregateModels;
using eCommerceBase.Persistence.UnitOfWork;
using eCommerceBase.Persistence.GenericRepository;
using eCommerceBase.Insfrastructure.Utilities.Caching.Redis;
using eCommerceBase.Application.Constants;
using eCommerceBase.Application.Handlers.Mapper;

namespace eCommerceBase.Application.Handlers.Warehouses.Commands;
public record UpdateWarehouseCommand(string Name,
		string Address,
		System.Guid Id) : IRequest<Result>;
public class UpdateWarehouseCommandHandler(IWriteDbRepository<Warehouse> warehouseRepository,
		IUnitOfWork unitOfWork,
		ICacheService cacheService) : IRequestHandler<UpdateWarehouseCommand,
		Result>
{
    private readonly IWriteDbRepository<Warehouse> _warehouseRepository = warehouseRepository;
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    private readonly ICacheService _cacheService = cacheService;
    public async Task<Result> Handle(UpdateWarehouseCommand request,
		CancellationToken cancellationToken)
    {
        return await _unitOfWork.BeginTransaction(async () =>
        {
            var data = await _warehouseRepository.GetAsync(u => u.Id == request.Id);
            if (data is not null)
            {
                data = WarehouseMapper.UpdateWarehouseCommandToWarehouse(request);
                _warehouseRepository.Update(data);
                await _cacheService.RemovePatternAsync("eCommerceBase:Warehouses");
                return Result.SuccessResult(Messages.Updated);
            }

            return Result.ErrorResult(Messages.UpdatedError);
        });
    }
}