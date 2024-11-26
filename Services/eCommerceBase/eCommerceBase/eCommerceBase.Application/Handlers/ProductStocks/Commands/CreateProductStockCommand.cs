using eCommerceBase.Application.Constants;
using eCommerceBase.Application.Handlers.Mapper;
using eCommerceBase.Domain.AggregateModels;
using eCommerceBase.Domain.Result;
using eCommerceBase.Insfrastructure.Utilities.Caching.Redis;
using eCommerceBase.Persistence.GenericRepository;
using eCommerceBase.Persistence.UnitOfWork;
using MediatR;

namespace eCommerceBase.Application.Handlers.ProductStocks.Commands;

public record CreateProductStockCommand(int RemainingStock,
		int TotalStock,
		Guid WarehouseId,
		Guid ProductId,
        double? Price,
        Guid CurrencyId,
        int? DiscountNumber,
        Guid? DiscountTypeId
        ) : IRequest<Result>;
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
            if(request.DiscountTypeId != null && request.DiscountNumber != 0)
            {
                var discountProduct = new DiscountProduct(request.DiscountTypeId.Value , data.Id, request.DiscountNumber ?? 0);
                data.AddDiscountProductList(discountProduct);
            }   
            await _productStockRepository.AddAsync(data);
            await _cacheService.RemovePatternAsync("eCommerceBase:Product");
            return Result.SuccessResult(Messages.Added);
        });
    }
}