using MediatR;
using eCommerceBase.Domain.Result;
using eCommerceBase.Domain.AggregateModels;
using eCommerceBase.Persistence.GenericRepository;
using eCommerceBase.Insfrastructure.Utilities.Caching.Redis;

namespace eCommerceBase.Application.Handlers.Discounts.Queries;
public record GetDiscountByIdQuery(System.Guid Id) : IRequest<Result<Discount>>;
public class GetDiscountByIdQueryHandler(IReadDbRepository<Discount> discountRepository,
		ICacheService cacheService) : IRequestHandler<GetDiscountByIdQuery,
		Result<Discount>>
{
    private readonly IReadDbRepository<Discount> _discountRepository = discountRepository;
    private readonly ICacheService _cacheService = cacheService;
    public async Task<Result<Discount>> Handle(GetDiscountByIdQuery request,
		CancellationToken cancellationToken)
    {
        return await _cacheService.GetAsync(request,
		async () =>
        {
            var query = await _discountRepository.GetByIdAsync(request.Id);
            return Result.SuccessDataResult<Discount>(query!);
        },
		cancellationToken);
    }
}