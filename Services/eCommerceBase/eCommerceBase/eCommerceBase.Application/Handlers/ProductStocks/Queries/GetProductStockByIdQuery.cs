using MediatR;
using eCommerceBase.Domain.Result;
using eCommerceBase.Domain.AggregateModels;
using eCommerceBase.Persistence.GenericRepository;
using eCommerceBase.Insfrastructure.Utilities.Caching.Redis;

namespace eCommerceBase.Application.Handlers.ProductStocks.Queries;
public record GetProductStockByIdQuery(System.Guid Id) : IRequest<Result<ProductStock>>;
public class GetProductStockByIdQueryHandler(IReadDbRepository<ProductStock> productStockRepository,
		ICacheService cacheService) : IRequestHandler<GetProductStockByIdQuery,
		Result<ProductStock>>
{
    private readonly IReadDbRepository<ProductStock> _productStockRepository = productStockRepository;
    private readonly ICacheService _cacheService = cacheService;
    public async Task<Result<ProductStock>> Handle(GetProductStockByIdQuery request,
		CancellationToken cancellationToken)
    {
        return await _cacheService.GetAsync(request,
		async () =>
        {
            var query = await _productStockRepository.GetByIdAsync(request.Id);
            return Result.SuccessDataResult<ProductStock>(query!);
        },
		cancellationToken);
    }
}