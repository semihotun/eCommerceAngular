using MediatR;
using eCommerceBase.Domain.Result;
using eCommerceBase.Domain.AggregateModels;
using eCommerceBase.Persistence.GenericRepository;
using eCommerceBase.Insfrastructure.Utilities.Caching.Redis;

namespace eCommerceBase.Application.Handlers.DiscountProducts.Queries;
public record GetDiscountProductByIdQuery(System.Guid Id) : IRequest<Result<DiscountProduct>>;
public class GetDiscountProductByIdQueryHandler(IReadDbRepository<DiscountProduct> discountProductRepository,
		ICacheService cacheService) : IRequestHandler<GetDiscountProductByIdQuery,
		Result<DiscountProduct>>
{
    private readonly IReadDbRepository<DiscountProduct> _discountProductRepository = discountProductRepository;
    private readonly ICacheService _cacheService = cacheService;
    public async Task<Result<DiscountProduct>> Handle(GetDiscountProductByIdQuery request,
		CancellationToken cancellationToken)
    {
        return await _cacheService.GetAsync(request,
		async () =>
        {
            var query = await _discountProductRepository.GetByIdAsync(request.Id);
            return Result.SuccessDataResult(query!);
        },
		cancellationToken);
    }
}