using MediatR;
using eCommerceBase.Domain.Result;
using eCommerceBase.Domain.AggregateModels;
using eCommerceBase.Persistence.GenericRepository;
using eCommerceBase.Insfrastructure.Utilities.Caching.Redis;

namespace eCommerceBase.Application.Handlers.ProductPhotoes.Queries;
public record GetAllProductPhoto() : IRequest<Result<IList<ProductPhoto>>>;
public class GetAllProductPhotoHandler(IReadDbRepository<ProductPhoto> productPhotoRepository,
		ICacheService cacheService) : IRequestHandler<GetAllProductPhoto,
		Result<IList<ProductPhoto>>>
{
    private readonly IReadDbRepository<ProductPhoto> _productPhotoRepository = productPhotoRepository;
    private readonly ICacheService _cacheService = cacheService;
    public async Task<Result<IList<ProductPhoto>>> Handle(GetAllProductPhoto request,
		CancellationToken cancellationToken)
    {
        return await _cacheService.GetAsync(request,
		async () =>
        {
            var data = await _productPhotoRepository.ToListAsync();
            return Result.SuccessDataResult(data!);
        },
		cancellationToken);
    }
}