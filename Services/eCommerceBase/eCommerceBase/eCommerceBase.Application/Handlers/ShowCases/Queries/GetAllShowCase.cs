using MediatR;
using eCommerceBase.Domain.Result;
using eCommerceBase.Domain.AggregateModels;
using eCommerceBase.Persistence.GenericRepository;
using eCommerceBase.Insfrastructure.Utilities.Caching.Redis;

namespace eCommerceBase.Application.Handlers.ShowCases.Queries;
public record GetAllShowCase() : IRequest<Result<IList<ShowCase>>>;
public class GetAllShowCaseHandler(IReadDbRepository<ShowCase> showCaseRepository,
		ICacheService cacheService) : IRequestHandler<GetAllShowCase,
		Result<IList<ShowCase>>>
{
    private readonly IReadDbRepository<ShowCase> _showCaseRepository = showCaseRepository;
    private readonly ICacheService _cacheService = cacheService;
    public async Task<Result<IList<ShowCase>>> Handle(GetAllShowCase request,
		CancellationToken cancellationToken)
    {
        return await _cacheService.GetAsync(request,
		async () =>
        {
            var data = await _showCaseRepository.ToListAsync();
            return Result.SuccessDataResult(data!);
        },
		cancellationToken);
    }
}