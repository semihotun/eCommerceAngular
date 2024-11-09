using MediatR;
using eCommerceBase.Domain.Result;
using eCommerceBase.Domain.AggregateModels;
using eCommerceBase.Persistence.GenericRepository;
using eCommerceBase.Insfrastructure.Utilities.Caching.Redis;

namespace eCommerceBase.Application.Handlers.ShowCaseTypes.Queries;
public record GetAllShowCaseType() : IRequest<Result<IList<ShowCaseType>>>;
public class GetAllShowCaseTypeHandler(IReadDbRepository<ShowCaseType> showCaseTypeRepository,
		ICacheService cacheService) : IRequestHandler<GetAllShowCaseType,
		Result<IList<ShowCaseType>>>
{
    private readonly IReadDbRepository<ShowCaseType> _showCaseTypeRepository = showCaseTypeRepository;
    private readonly ICacheService _cacheService = cacheService;
    public async Task<Result<IList<ShowCaseType>>> Handle(GetAllShowCaseType request,
		CancellationToken cancellationToken)
    {
        return await _cacheService.GetAsync(request,
		async () =>
        {
            var data = await _showCaseTypeRepository.ToListAsync();
            return Result.SuccessDataResult(data!);
        },
		cancellationToken);
    }
}