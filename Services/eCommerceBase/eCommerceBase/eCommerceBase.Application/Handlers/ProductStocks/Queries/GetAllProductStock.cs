using MediatR;
using eCommerceBase.Domain.Result;
using eCommerceBase.Domain.AggregateModels;
using eCommerceBase.Persistence.GenericRepository;
using eCommerceBase.Insfrastructure.Utilities.Caching.Redis;

namespace eCommerceBase.Application.Handlers.ProductStocks.Queries;
public record GetAllProductStock() : IRequest<Result<IList<ProductStock>>>;
public class GetAllProductStockHandler(IReadDbRepository<ProductStock> productStockRepository,
		ICacheService cacheService) : IRequestHandler<GetAllProductStock,
		Result<IList<ProductStock>>>
{
    private readonly IReadDbRepository<ProductStock> _productStockRepository = productStockRepository;
    private readonly ICacheService _cacheService = cacheService;
    public async Task<Result<IList<ProductStock>>> Handle(GetAllProductStock request,
		CancellationToken cancellationToken)
    {
        return await _cacheService.GetAsync(request,
		async () =>
        {
            var data = await _productStockRepository.ToListAsync();
            return Result.SuccessDataResult(data!);
        },
		cancellationToken);
    }
}