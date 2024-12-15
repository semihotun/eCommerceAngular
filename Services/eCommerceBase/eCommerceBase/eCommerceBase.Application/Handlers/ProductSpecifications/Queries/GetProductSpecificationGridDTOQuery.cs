using eCommerceBase.Application.Handlers.ProductSpecifications.Queries.Dtos;
using eCommerceBase.Domain.AggregateModels;
using eCommerceBase.Domain.Result;
using eCommerceBase.Insfrastructure.Utilities.Caching.Redis;
using eCommerceBase.Insfrastructure.Utilities.Grid.PagedList;
using eCommerceBase.Persistence.GenericRepository;
using MediatR;

namespace eCommerceBase.Application.Handlers.ProductSpecifications.Queries;
public record GetProductDetailSpeficationListDTOQuery(Guid? ProductId, int? PageIndex, int PageSize)
    : IRequest<Result<PagedList<ProductDetailSpeficationListDTO>>>;
public class GetProductDetailSpeficationListDTOQueryHandler(IReadDbRepository<ProductSpecification> productSpecificationRepository, ICacheService cacheService)
    : IRequestHandler<GetProductDetailSpeficationListDTOQuery, Result<PagedList<ProductDetailSpeficationListDTO>>>
{
    private readonly IReadDbRepository<ProductSpecification> _productSpecificationRepository = productSpecificationRepository;
    private readonly ICacheService _cacheService = cacheService;
    public async Task<Result<PagedList<ProductDetailSpeficationListDTO>>> Handle(GetProductDetailSpeficationListDTOQuery request,
        CancellationToken cancellationToken)
    {
        return await _cacheService.GetAsync(request, async () =>
        {
            var query = await _productSpecificationRepository.Query()
            .Where(x => x.ProductId == request.ProductId)
            .Select(x => new ProductDetailSpeficationListDTO
            {
                Id = x.Id,
                SpecificationAttributeOptionName = x.SpecificationAttributeOption!.Name,
                SpecificationAttributeName = x.SpecificationAttributeOption!.SpecificationAttribute!.Name
            }).ToPagedListAsync(1, 100);

            return Result.SuccessDataResult(query);
        }, cancellationToken);
    }
}