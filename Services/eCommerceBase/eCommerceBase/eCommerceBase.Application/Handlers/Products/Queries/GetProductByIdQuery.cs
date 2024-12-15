using MediatR;
using eCommerceBase.Domain.Result;
using eCommerceBase.Domain.AggregateModels;
using eCommerceBase.Persistence.GenericRepository;
using eCommerceBase.Insfrastructure.Utilities.Caching.Redis;

namespace eCommerceBase.Application.Handlers.Products.Queries;
public record GetProductByIdQuery(System.Guid Id) : IRequest<Result<Product>>;
public class GetProductByIdQueryHandler(IReadDbRepository<Product> productRepository,
		ICacheService cacheService) : IRequestHandler<GetProductByIdQuery,
		Result<Product>>
{
    private readonly IReadDbRepository<Product> _productRepository = productRepository;
    private readonly ICacheService _cacheService = cacheService;
    public async Task<Result<Product>> Handle(GetProductByIdQuery request,
		CancellationToken cancellationToken)
    {
        return await _cacheService.GetAsync(request,
		async () =>
        {
            var query = await _productRepository.GetByIdAsync(request.Id);
            return Result.SuccessDataResult(query!);
        },
		cancellationToken);
    }
}