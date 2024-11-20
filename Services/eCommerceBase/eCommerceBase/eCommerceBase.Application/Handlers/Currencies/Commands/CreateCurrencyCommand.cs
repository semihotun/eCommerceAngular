using MediatR;
using eCommerceBase.Domain.Result;
using eCommerceBase.Domain.AggregateModels;
using eCommerceBase.Persistence.UnitOfWork;
using eCommerceBase.Persistence.GenericRepository;
using eCommerceBase.Insfrastructure.Utilities.Caching.Redis;
using eCommerceBase.Application.Constants;
using eCommerceBase.Application.Handlers.Mapper;

namespace eCommerceBase.Application.Handlers.Currencies.Commands;
public record CreateCurrencyCommand(string? Symbol,
		string? Code,
		string? Name) : IRequest<Result>;
public class CreateCurrencyCommandHandler(IWriteDbRepository<Currency> currencyRepository,
		IUnitOfWork unitOfWork,
		ICacheService cacheService) : IRequestHandler<CreateCurrencyCommand,
		Result>
{
    private readonly IWriteDbRepository<Currency> _currencyRepository = currencyRepository;
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    private readonly ICacheService _cacheService = cacheService;
    public async Task<Result> Handle(CreateCurrencyCommand request,
		CancellationToken cancellationToken)
    {
        return await _unitOfWork.BeginTransaction(async () =>
        {
            var data = CurrencyMapper.CreateCurrencyCommandToCurrency(request);
            await _currencyRepository.AddAsync(data);
            await _cacheService.RemovePatternAsync("eCommerceBase:Currencies");
            return Result.SuccessResult(Messages.Added);
        });
    }
}