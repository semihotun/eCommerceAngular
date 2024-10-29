using MediatR;
using eCommerceBase.Domain.Result;
using eCommerceBase.Domain.AggregateModels;
using eCommerceBase.Persistence.GenericRepository;
using eCommerceBase.Insfrastructure.Utilities.Caching.Redis;

namespace eCommerceBase.Application.Handlers.ProductSpecifications.Queries;
public record GetProductSpecificationByIdQuery(System.Guid Id) : IRequest<Result<ProductSpecification>>;
public class GetProductSpecificationByIdQueryHandler(IReadDbRepository<ProductSpecification> productSpecificationRepository,
		ICacheService cacheService) : IRequestHandler<GetProductSpecificationByIdQuery,
		Result<ProductSpecification>>
{
    private readonly IReadDbRepository<ProductSpecification> _productSpecificationRepository = productSpecificationRepository;
    private readonly ICacheService _cacheService = cacheService;
    public async Task<Result<ProductSpecification>> Handle(GetProductSpecificationByIdQuery request,
		CancellationToken cancellationToken)
    {
        return await _cacheService.GetAsync(request,
		async () =>
        {
            var query = await _productSpecificationRepository.GetByIdAsync(request.Id);
            return Result.SuccessDataResult<ProductSpecification>(query!);
        },
		cancellationToken);
    }
}