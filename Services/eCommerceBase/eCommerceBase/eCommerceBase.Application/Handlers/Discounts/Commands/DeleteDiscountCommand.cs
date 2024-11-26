using MediatR;
using eCommerceBase.Domain.Result;
using eCommerceBase.Domain.AggregateModels;
using eCommerceBase.Persistence.UnitOfWork;
using eCommerceBase.Persistence.GenericRepository;
using eCommerceBase.Insfrastructure.Utilities.Caching.Redis;
using eCommerceBase.Application.Constants;

namespace eCommerceBase.Application.Handlers.Discounts.Commands;
public record DeleteDiscountCommand(System.Guid Id) : IRequest<Result>;
public class DeleteDiscountCommandHandler(IWriteDbRepository<Discount> discountRepository,
        IUnitOfWork unitOfWork,
        ICacheService cacheService,
           IWriteDbRepository<DiscountProduct> discountProductRepository) : IRequestHandler<DeleteDiscountCommand,
        Result>
{
    private readonly IWriteDbRepository<Discount> _discountRepository = discountRepository;
    private readonly IWriteDbRepository<DiscountProduct> _discountProductRepository = discountProductRepository;
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    private readonly ICacheService _cacheService = cacheService;
    public async Task<Result> Handle(DeleteDiscountCommand request,
        CancellationToken cancellationToken)
    {
        return await _unitOfWork.BeginTransaction(async () =>
        {
            var data = await _discountRepository.GetAsync(p => p.Id == request.Id);
            if (data is not null)
            {
                data.Deleted = true;
                _discountRepository.Update(data);

                var discountProducts = await _discountProductRepository.ToListAsync(x => x.DiscountId == data.Id);
                foreach (var discountProduct in discountProducts)
                {
                    discountProduct.Deleted = true;
                    _discountProductRepository.Update(discountProduct);
                }
                await _cacheService.RemovePatternAsync("eCommerceBase:Discount");
                await _cacheService.RemovePatternAsync("eCommerceBase:Product");
                await _cacheService.RemovePatternAsync("eCommerceBase:Showcase");
                return Result.SuccessResult(Messages.Deleted);
            }
            else
            {
                return Result.ErrorResult(Messages.DeletedError);
            }
        });
    }
}