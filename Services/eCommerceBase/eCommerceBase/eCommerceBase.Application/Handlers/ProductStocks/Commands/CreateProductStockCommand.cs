using MediatR;
using eCommerceBase.Domain.Result;
using eCommerceBase.Domain.AggregateModels;
using eCommerceBase.Persistence.UnitOfWork;
using eCommerceBase.Persistence.GenericRepository;
using eCommerceBase.Insfrastructure.Utilities.Caching.Redis;
using eCommerceBase.Application.Constants;
using eCommerceBase.Application.Handlers.Mapper;

namespace eCommerceBase.Application.Handlers.ProductStocks.Commands;
public record CreateProductStockCommand(int RemainingStock,
		int TotalStock,
		Guid WarehouseId,
		Guid ProductId) : IRequest<Result>;
public class CreateProductStockCommandHandler(IWriteDbRepository<ProductStock> productStockRepository,
		IUnitOfWork unitOfWork,
		ICacheService cacheService) : IRequestHandler<CreateProductStockCommand,
		Result>
{
    private readonly IWriteDbRepository<ProductStock> _productStockRepository = productStockRepository;
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    private readonly ICacheService _cacheService = cacheService;
    public async Task<Result> Handle(CreateProductStockCommand request,
		CancellationToken cancellationToken)
    {
        return await _unitOfWork.BeginTransaction(async () =>
        {
            var data = ProductStockMapper.CreateProductStockCommandToProductStock(request);
            await _productStockRepository.AddAsync(data);
            await _cacheService.RemovePatternAsync("eCommerceBase:ProductStocks");
            return Result.SuccessResult(Messages.Added);
        });
    }
}