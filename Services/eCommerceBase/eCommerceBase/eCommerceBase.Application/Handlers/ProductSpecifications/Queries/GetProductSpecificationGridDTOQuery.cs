using MediatR;
using eCommerceBase.Domain.AggregateModels;
using eCommerceBase.Insfrastructure.Utilities.Grid.PagedList;
using eCommerceBase.Domain.Result;
using eCommerceBase.Application.Handlers.ProductSpecifications.Queries.Dtos;
using eCommerceBase.Insfrastructure.Utilities.Caching.Redis;
using eCommerceBase.Persistence.Context;
using Microsoft.EntityFrameworkCore;

namespace eCommerceBase.Application.Handlers.ProductSpecifications.Queries;
public record GetProductDetailSpeficationListDTOQuery(Guid? ProductId, int? PageIndex, int PageSize)
    : IRequest<Result<PagedList<ProductDetailSpeficationListDTO>>>;
public class GetProductDetailSpeficationListDTOQueryHandler(CoreDbContext coreDbContext, ICacheService cacheService)
    : IRequestHandler<GetProductDetailSpeficationListDTOQuery, Result<PagedList<ProductDetailSpeficationListDTO>>>
{
    private readonly CoreDbContext _coreDbContext = coreDbContext;
    private readonly ICacheService _cacheService = cacheService;
    public async Task<Result<PagedList<ProductDetailSpeficationListDTO>>> Handle(GetProductDetailSpeficationListDTOQuery request,
        CancellationToken cancellationToken)
    {
        return await _cacheService.GetAsync(request, async () =>
        {
            var query = await _coreDbContext.Query<ProductSpecification>()
            .Include(x => x.SpecificationAttributeOption)
            .Include(x => x.SpecificationAttributeOption!.SpecificationAttribute)
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