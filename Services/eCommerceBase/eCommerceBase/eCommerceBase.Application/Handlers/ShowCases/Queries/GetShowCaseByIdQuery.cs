using MediatR;
using eCommerceBase.Domain.Result;
using eCommerceBase.Domain.AggregateModels;
using eCommerceBase.Persistence.GenericRepository;
using eCommerceBase.Insfrastructure.Utilities.Caching.Redis;

namespace eCommerceBase.Application.Handlers.ShowCases.Queries;
public record GetShowCaseByIdQuery(System.Guid Id) : IRequest<Result<ShowCase>>;
public class GetShowCaseByIdQueryHandler(IReadDbRepository<ShowCase> showCaseRepository,
		ICacheService cacheService) : IRequestHandler<GetShowCaseByIdQuery,
		Result<ShowCase>>
{
    private readonly IReadDbRepository<ShowCase> _showCaseRepository = showCaseRepository;
    private readonly ICacheService _cacheService = cacheService;
    public async Task<Result<ShowCase>> Handle(GetShowCaseByIdQuery request,
		CancellationToken cancellationToken)
    {
        return await _cacheService.GetAsync(request,
		async () =>
        {
            var query = await _showCaseRepository.GetByIdAsync(request.Id);
            return Result.SuccessDataResult(query!);
        },
		cancellationToken);
    }
}