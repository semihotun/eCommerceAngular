using MediatR;
using eCommerceBase.Domain.Result;
using eCommerceBase.Domain.AggregateModels;
using eCommerceBase.Persistence.GenericRepository;
using eCommerceBase.Insfrastructure.Utilities.Caching.Redis;

namespace eCommerceBase.Application.Handlers.DiscountProducts.Queries;
public record GetAllDiscountProduct() : IRequest<Result<IList<DiscountProduct>>>;
public class GetAllDiscountProductHandler(IReadDbRepository<DiscountProduct> discountProductRepository,
		ICacheService cacheService) : IRequestHandler<GetAllDiscountProduct,
		Result<IList<DiscountProduct>>>
{
    private readonly IReadDbRepository<DiscountProduct> _discountProductRepository = discountProductRepository;
    private readonly ICacheService _cacheService = cacheService;
    public async Task<Result<IList<DiscountProduct>>> Handle(GetAllDiscountProduct request,
		CancellationToken cancellationToken)
    {
        return await _cacheService.GetAsync(request,
		async () =>
        {
            var data = await _discountProductRepository.ToListAsync();
            return Result.SuccessDataResult(data!);
        },
		cancellationToken);
    }
}