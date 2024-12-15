using MediatR;
using eCommerceBase.Domain.Result;
using eCommerceBase.Domain.AggregateModels;
using eCommerceBase.Persistence.GenericRepository;
using eCommerceBase.Insfrastructure.Utilities.Caching.Redis;

namespace eCommerceBase.Application.Handlers.Currencies.Queries;
public record GetCurrencyByIdQuery(System.Guid Id) : IRequest<Result<Currency>>;
public class GetCurrencyByIdQueryHandler(IReadDbRepository<Currency> currencyRepository,
		ICacheService cacheService) : IRequestHandler<GetCurrencyByIdQuery,
		Result<Currency>>
{
    private readonly IReadDbRepository<Currency> _currencyRepository = currencyRepository;
    private readonly ICacheService _cacheService = cacheService;
    public async Task<Result<Currency>> Handle(GetCurrencyByIdQuery request,
		CancellationToken cancellationToken)
    {
        return await _cacheService.GetAsync(request,
		async () =>
        {
            var query = await _currencyRepository.GetByIdAsync(request.Id);
            return Result.SuccessDataResult(query!);
        },
		cancellationToken);
    }
}