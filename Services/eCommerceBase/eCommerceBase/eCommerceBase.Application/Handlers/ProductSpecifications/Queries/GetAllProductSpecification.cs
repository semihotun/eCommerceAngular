using eCommerceBase.Domain.AggregateModels;
using eCommerceBase.Domain.Result;
using eCommerceBase.Insfrastructure.Utilities.Caching.Redis;
using eCommerceBase.Persistence.GenericRepository;
using MediatR;

namespace eCommerceBase.Application.Handlers.ProductSpecifications.Queries;
public record GetAllProductSpecification() : IRequest<Result<IList<ProductSpecification>>>;
public class GetAllProductSpecificationHandler(IReadDbRepository<ProductSpecification> productSpecificationRepository,
		ICacheService cacheService) : IRequestHandler<GetAllProductSpecification,
		Result<IList<ProductSpecification>>>
{
    private readonly IReadDbRepository<ProductSpecification> _productSpecificationRepository = productSpecificationRepository;
    private readonly ICacheService _cacheService = cacheService;
    public async Task<Result<IList<ProductSpecification>>> Handle(GetAllProductSpecification request,
		CancellationToken cancellationToken)
    {
        return await _cacheService.GetAsync(request,
		async () =>
        {
            var data = await _productSpecificationRepository.ToListAsync();
            return Result.SuccessDataResult(data!);
        },
		cancellationToken);
    }
}