using MediatR;
using eCommerceBase.Domain.Result;
using eCommerceBase.Domain.AggregateModels;
using eCommerceBase.Persistence.GenericRepository;
using eCommerceBase.Insfrastructure.Utilities.Caching.Redis;

namespace eCommerceBase.Application.Handlers.Pages.Queries;
public record GetPageByIdQuery(System.Guid Id) : IRequest<Result<Page>>;
public class GetPageByIdQueryHandler(IReadDbRepository<Page> pageRepository,
		ICacheService cacheService) : IRequestHandler<GetPageByIdQuery,
		Result<Page>>
{
    private readonly IReadDbRepository<Page> _pageRepository = pageRepository;
    private readonly ICacheService _cacheService = cacheService;
    public async Task<Result<Page>> Handle(GetPageByIdQuery request,
		CancellationToken cancellationToken)
    {
        return await _cacheService.GetAsync(request,
		async () =>
        {
            var query = await _pageRepository.GetByIdAsync(request.Id);
            return Result.SuccessDataResult(query!);
        },
		cancellationToken);
    }
}