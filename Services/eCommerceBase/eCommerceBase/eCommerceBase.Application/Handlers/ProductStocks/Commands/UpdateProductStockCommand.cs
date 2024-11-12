using MediatR;
using eCommerceBase.Domain.Result;
using eCommerceBase.Domain.AggregateModels;
using eCommerceBase.Persistence.UnitOfWork;
using eCommerceBase.Persistence.GenericRepository;
using eCommerceBase.Insfrastructure.Utilities.Caching.Redis;
using eCommerceBase.Application.Constants;
using eCommerceBase.Application.Handlers.Mapper;

namespace eCommerceBase.Application.Handlers.ProductStocks.Commands;
public record UpdateProductStockCommand(int RemainingStock,
		int TotalStock,
		Guid WarehouseId,
		Guid ProductId,
		System.Guid Id) : IRequest<Result>;
public class UpdateProductStockCommandHandler(IWriteDbRepository<ProductStock> productStockRepository,
		IUnitOfWork unitOfWork,
		ICacheService cacheService) : IRequestHandler<UpdateProductStockCommand,
		Result>
{
    private readonly IWriteDbRepository<ProductStock> _productStockRepository = productStockRepository;
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    private readonly ICacheService _cacheService = cacheService;
    public async Task<Result> Handle(UpdateProductStockCommand request,
		CancellationToken cancellationToken)
    {
        return await _unitOfWork.BeginTransaction(async () =>
        {
            var data = await _productStockRepository.GetAsync(u => u.Id == request.Id);
            if (data is not null)
            {
                data = ProductStockMapper.UpdateProductStockCommandToProductStock(request);
                _productStockRepository.Update(data);
                await _cacheService.RemovePatternAsync("eCommerceBase:ProductStocks");
                return Result.SuccessResult(Messages.Updated);
            }

            return Result.ErrorResult(Messages.UpdatedError);
        });
    }
}