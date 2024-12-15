using MediatR;
using eCommerceBase.Domain.Result;
using eCommerceBase.Domain.AggregateModels;
using eCommerceBase.Persistence.GenericRepository;
using eCommerceBase.Insfrastructure.Utilities.Caching.Redis;

namespace eCommerceBase.Application.Handlers.ProductPhotoes.Queries;
public record GetProductPhotoByIdQuery(System.Guid Id) : IRequest<Result<ProductPhoto>>;
public class GetProductPhotoByIdQueryHandler(IReadDbRepository<ProductPhoto> productPhotoRepository,
		ICacheService cacheService) : IRequestHandler<GetProductPhotoByIdQuery,
		Result<ProductPhoto>>
{
    private readonly IReadDbRepository<ProductPhoto> _productPhotoRepository = productPhotoRepository;
    private readonly ICacheService _cacheService = cacheService;
    public async Task<Result<ProductPhoto>> Handle(GetProductPhotoByIdQuery request,
		CancellationToken cancellationToken)
    {
        return await _cacheService.GetAsync(request,
		async () =>
        {
            var query = await _productPhotoRepository.GetByIdAsync(request.Id);
            return Result.SuccessDataResult(query!);
        },
		cancellationToken);
    }
}