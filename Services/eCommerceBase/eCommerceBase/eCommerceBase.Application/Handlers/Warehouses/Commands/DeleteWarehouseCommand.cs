using MediatR;
using eCommerceBase.Domain.Result;
using eCommerceBase.Domain.AggregateModels;
using eCommerceBase.Persistence.UnitOfWork;
using eCommerceBase.Persistence.GenericRepository;
using eCommerceBase.Insfrastructure.Utilities.Caching.Redis;
using eCommerceBase.Application.Constants;

namespace eCommerceBase.Application.Handlers.Warehouses.Commands;
public record DeleteWarehouseCommand(System.Guid Id) : IRequest<Result>;
public class DeleteWarehouseCommandHandler(IWriteDbRepository<Warehouse> warehouseRepository,
		IUnitOfWork unitOfWork,
		ICacheService cacheService) : IRequestHandler<DeleteWarehouseCommand,
		Result>
{
    private readonly IWriteDbRepository<Warehouse> _warehouseRepository = warehouseRepository;
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    private readonly ICacheService _cacheService = cacheService;
    public async Task<Result> Handle(DeleteWarehouseCommand request,
		CancellationToken cancellationToken)
    {
        return await _unitOfWork.BeginTransaction(async () =>
        {
            var data = await _warehouseRepository.GetAsync(p => p.Id == request.Id);
            if (data is not null)
            {
                data.Deleted = true;
                _warehouseRepository.Update(data);
                await _cacheService.RemovePatternAsync("eCommerceBase:Warehouses");
                return Result.SuccessResult(Messages.Deleted);
            }
            else
            {
                return Result.ErrorResult(Messages.DeletedError);
            }
        });
    }
}