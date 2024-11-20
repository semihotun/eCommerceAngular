using MediatR;
using eCommerceBase.Domain.Result;
using eCommerceBase.Domain.AggregateModels;
using eCommerceBase.Persistence.UnitOfWork;
using eCommerceBase.Persistence.GenericRepository;
using eCommerceBase.Insfrastructure.Utilities.Caching.Redis;
using eCommerceBase.Application.Constants;
using eCommerceBase.Application.Handlers.Mapper;

namespace eCommerceBase.Application.Handlers.Currencies.Commands;
public record UpdateCurrencyCommand(string? Symbol,
		string? Code,
		string? Name,
		System.Guid Id) : IRequest<Result>;
public class UpdateCurrencyCommandHandler(IWriteDbRepository<Currency> currencyRepository,
		IUnitOfWork unitOfWork,
		ICacheService cacheService) : IRequestHandler<UpdateCurrencyCommand,
		Result>
{
    private readonly IWriteDbRepository<Currency> _currencyRepository = currencyRepository;
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    private readonly ICacheService _cacheService = cacheService;
    public async Task<Result> Handle(UpdateCurrencyCommand request,
		CancellationToken cancellationToken)
    {
        return await _unitOfWork.BeginTransaction(async () =>
        {
            var data = await _currencyRepository.GetAsync(u => u.Id == request.Id);
            if (data is not null)
            {
                data = CurrencyMapper.UpdateCurrencyCommandToCurrency(request);
                _currencyRepository.Update(data);
                await _cacheService.RemovePatternAsync("eCommerceBase:Currencies");
                return Result.SuccessResult(Messages.Updated);
            }

            return Result.ErrorResult(Messages.UpdatedError);
        });
    }
}