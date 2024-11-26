using MediatR;
using eCommerceBase.Domain.Result;
using eCommerceBase.Domain.AggregateModels;
using eCommerceBase.Persistence.UnitOfWork;
using eCommerceBase.Persistence.GenericRepository;
using eCommerceBase.Insfrastructure.Utilities.Caching.Redis;
using eCommerceBase.Application.Constants;
using eCommerceBase.Application.Handlers.Mapper;

namespace eCommerceBase.Application.Handlers.DiscountProducts.Commands;
public record CreateDiscountProductCommand(Guid DiscountId,
		Guid ProductStockId,
		int DiscountNumber,
		ProductStock? ProductStock,
		Discount? Discount) : IRequest<Result>;
public class CreateDiscountProductCommandHandler(IWriteDbRepository<DiscountProduct> discountProductRepository,
		IUnitOfWork unitOfWork,
		ICacheService cacheService) : IRequestHandler<CreateDiscountProductCommand,
		Result>
{
    private readonly IWriteDbRepository<DiscountProduct> _discountProductRepository = discountProductRepository;
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    private readonly ICacheService _cacheService = cacheService;
    public async Task<Result> Handle(CreateDiscountProductCommand request,
		CancellationToken cancellationToken)
    {
        return await _unitOfWork.BeginTransaction(async () =>
        {
            var data = DiscountProductMapper.CreateDiscountProductCommandToDiscountProduct(request);
            await _discountProductRepository.AddAsync(data);
            await _cacheService.RemovePatternAsync("eCommerceBase:DiscountProducts");
            return Result.SuccessResult(Messages.Added);
        });
    }
}