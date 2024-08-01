using MediatR;
using eCommerceBase.Domain.Result;
using eCommerceBase.Domain.AggregateModels;
using eCommerceBase.Persistence.GenericRepository;
using eCommerceBase.Insfrastructure.Utilities.Caching.Redis;

namespace eCommerceBase.Application.Handlers.Pages.Queries;
public record GetAllPage() : IRequest<Result<IList<Page>>>;
public class GetAllPageHandler(IReadDbRepository<Page> pageRepository,
		ICacheService cacheService) : IRequestHandler<GetAllPage,
		Result<IList<Page>>>
{
    private readonly IReadDbRepository<Page> _pageRepository = pageRepository;
    private readonly ICacheService _cacheService = cacheService;
    public async Task<Result<IList<Page>>> Handle(GetAllPage request,
		CancellationToken cancellationToken)
    {
        return await _cacheService.GetAsync(request,
		async () =>
        {
            var data = await _pageRepository.ToListAsync();
            return Result.SuccessDataResult(data!);
        },
		cancellationToken);
    }
}