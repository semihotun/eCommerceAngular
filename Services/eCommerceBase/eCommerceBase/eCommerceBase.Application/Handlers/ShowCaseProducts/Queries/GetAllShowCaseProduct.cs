using MediatR;
using eCommerceBase.Domain.Result;
using eCommerceBase.Domain.AggregateModels;
using eCommerceBase.Persistence.GenericRepository;
using eCommerceBase.Insfrastructure.Utilities.Caching.Redis;

namespace eCommerceBase.Application.Handlers.ShowCaseProducts.Queries;
public record GetAllShowCaseProduct() : IRequest<Result<IList<ShowCaseProduct>>>;
public class GetAllShowCaseProductHandler(IReadDbRepository<ShowCaseProduct> showCaseProductRepository,
		ICacheService cacheService) : IRequestHandler<GetAllShowCaseProduct,
		Result<IList<ShowCaseProduct>>>
{
    private readonly IReadDbRepository<ShowCaseProduct> _showCaseProductRepository = showCaseProductRepository;
    private readonly ICacheService _cacheService = cacheService;
    public async Task<Result<IList<ShowCaseProduct>>> Handle(GetAllShowCaseProduct request,
		CancellationToken cancellationToken)
    {
        return await _cacheService.GetAsync(request,
		async () =>
        {
            var data = await _showCaseProductRepository.ToListAsync();
            return Result.SuccessDataResult(data!);
        },
		cancellationToken);
    }
}