using MediatR;
using eCommerceBase.Domain.Result;
using eCommerceBase.Domain.AggregateModels;
using eCommerceBase.Persistence.UnitOfWork;
using eCommerceBase.Persistence.GenericRepository;
using eCommerceBase.Insfrastructure.Utilities.Caching.Redis;
using eCommerceBase.Application.Constants;
using eCommerceBase.Application.Handlers.Mapper;

namespace eCommerceBase.Application.Handlers.Discounts.Commands;
public record CreateDiscountCommand(
        Guid? CategoryId,
        string? Name,
		Guid DiscountTypeId,
        double DiscountNumber,
		DiscountType? DiscountType) : IRequest<Result>;
public class CreateDiscountCommandHandler(IWriteDbRepository<Discount> discountRepository,
    IWriteDbRepository<ProductStock> productStockRepository,
    IWriteDbRepository<DiscountProduct> discountProductRepository,
        IUnitOfWork unitOfWork,
		ICacheService cacheService) : IRequestHandler<CreateDiscountCommand,
		Result>
{
    private readonly IWriteDbRepository<Discount> _discountRepository = discountRepository;
    private readonly IWriteDbRepository<DiscountProduct> _discountProductRepository = discountProductRepository;
    private readonly IWriteDbRepository<ProductStock> _productStockRepository = productStockRepository;
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    private readonly ICacheService _cacheService = cacheService;
    public async Task<Result> Handle(CreateDiscountCommand request,
		CancellationToken cancellationToken)
    {
        return await _unitOfWork.BeginTransaction(async () =>
        {
            var data = DiscountMapper.CreateDiscountCommandToDiscount(request);
            await _discountRepository.AddAsync(data);
            if (request.CategoryId != null)
            {
                var productStockList = await _productStockRepository.ToListAsync(x => x.Product!.CategoryId == request.CategoryId,x=>x.Product!);
                foreach (var productStock in productStockList)
                {
                    var discountProduct = new DiscountProduct(data.Id,productStock.Id,request.DiscountNumber);
                    productStock.AddDiscountProductList(discountProduct);
                    await _discountProductRepository.AddAsync(discountProduct);
                }
            }
            await _cacheService.RemovePatternAsync("eCommerceBase:Discount");
            await _cacheService.RemovePatternAsync("eCommerceBase:Product");
            await _cacheService.RemovePatternAsync("eCommerceBase:Showcase");
            return Result.SuccessResult(Messages.Added);
        });
    }
}