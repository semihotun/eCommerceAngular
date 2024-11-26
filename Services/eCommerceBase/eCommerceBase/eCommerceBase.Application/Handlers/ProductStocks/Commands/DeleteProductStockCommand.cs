using MediatR;
using eCommerceBase.Domain.Result;
using eCommerceBase.Domain.AggregateModels;
using eCommerceBase.Persistence.UnitOfWork;
using eCommerceBase.Persistence.GenericRepository;
using eCommerceBase.Insfrastructure.Utilities.Caching.Redis;
using eCommerceBase.Application.Constants;

namespace eCommerceBase.Application.Handlers.ProductStocks.Commands;
public record DeleteProductStockCommand(System.Guid Id) : IRequest<Result>;
public class DeleteProductStockCommandHandler(IWriteDbRepository<ProductStock> productStockRepository,
    IWriteDbRepository<DiscountProduct> discountProductRepository,

        IUnitOfWork unitOfWork,
		ICacheService cacheService) : IRequestHandler<DeleteProductStockCommand,
		Result>
{
    private readonly IWriteDbRepository<ProductStock> _productStockRepository = productStockRepository;
    private readonly IWriteDbRepository<DiscountProduct> _discountProductRepository = discountProductRepository;
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    private readonly ICacheService _cacheService = cacheService;
    public async Task<Result> Handle(DeleteProductStockCommand request,
		CancellationToken cancellationToken)
    {
        return await _unitOfWork.BeginTransaction(async () =>
        {
            var data = await _productStockRepository.GetAsync(p => p.Id == request.Id,x=>x.DiscountProductList);
            if (data is not null)
            {
                data.Deleted = true;
                _productStockRepository.Update(data);

               foreach(var item in data.DiscountProductList)
                {
                    item.Deleted = true;
                    _discountProductRepository.Update(item);
                }
                await _cacheService.RemovePatternAsync("eCommerceBase:Product");
                return Result.SuccessResult(Messages.Deleted);
            }
            else
            {
                return Result.ErrorResult(Messages.DeletedError);
            }
        });
    }
}