using eCommerceBase.Domain.AggregateModels;
using eCommerceBase.Domain.Result;
using eCommerceBase.Insfrastructure.Utilities.Caching.Redis;
using eCommerceBase.Persistence.GenericRepository;
using MediatR;

namespace eCommerceBase.Application.Handlers.Products.Queries;
public record GetAllProduct() : IRequest<Result<IList<Product>>>;
public class GetAllProductHandler(IReadDbRepository<Product> productRepository,
		ICacheService cacheService) : IRequestHandler<GetAllProduct,
		Result<IList<Product>>>
{
    private readonly IReadDbRepository<Product> _productRepository = productRepository;
    private readonly ICacheService _cacheService = cacheService;
    public async Task<Result<IList<Product>>> Handle(GetAllProduct request,
		CancellationToken cancellationToken)
    {
        return await _cacheService.GetAsync(request,
		async () =>
        {
            var data = await _productRepository.ToListAsync();
            return Result.SuccessDataResult(data!);
        },
		cancellationToken);
    }
}