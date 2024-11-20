using MediatR;
using eCommerceBase.Domain.Result;
using eCommerceBase.Domain.AggregateModels;
using eCommerceBase.Persistence.GenericRepository;
using eCommerceBase.Insfrastructure.Utilities.Caching.Redis;

namespace eCommerceBase.Application.Handlers.Currencies.Queries;
public record GetAllCurrency() : IRequest<Result<IList<Currency>>>;
public class GetAllCurrencyHandler(IReadDbRepository<Currency> currencyRepository,
		ICacheService cacheService) : IRequestHandler<GetAllCurrency,
		Result<IList<Currency>>>
{
    private readonly IReadDbRepository<Currency> _currencyRepository = currencyRepository;
    private readonly ICacheService _cacheService = cacheService;
    public async Task<Result<IList<Currency>>> Handle(GetAllCurrency request,
		CancellationToken cancellationToken)
    {
        return await _cacheService.GetAsync(request,
		async () =>
        {
            var data = await _currencyRepository.ToListAsync();
            return Result.SuccessDataResult(data!);
        },
		cancellationToken);
    }
}