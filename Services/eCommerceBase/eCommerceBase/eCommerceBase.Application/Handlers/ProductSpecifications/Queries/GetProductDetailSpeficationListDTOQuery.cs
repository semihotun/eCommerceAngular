using eCommerceBase.Application.Handlers.ProductSpecifications.Queries.Dtos;
using eCommerceBase.Domain.AggregateModels;
using eCommerceBase.Domain.Result;
using eCommerceBase.Insfrastructure.Utilities.Caching.Redis;
using eCommerceBase.Insfrastructure.Utilities.Grid.Filter;
using eCommerceBase.Insfrastructure.Utilities.Grid.PagedList;
using eCommerceBase.Persistence.GenericRepository;
using MediatR;

namespace eCommerceBase.Application.Handlers.ProductSpecifications.Queries;
public record GetProductSpecificationGridDTOQuery(Guid ProductId, int PageIndex, int PageSize, string? OrderByColumnName, List<FilterModel>? FilterModelList)
    : IRequest<Result<PagedList<ProductSpecificationGridDTO>>>;
public class GetProductSpecificationGridDTOQueryHandler(IReadDbRepository<ProductSpecification> productSpecificationRepository, ICacheService cacheService)
    : IRequestHandler<GetProductSpecificationGridDTOQuery, Result<PagedList<ProductSpecificationGridDTO>>>
{
    private readonly IReadDbRepository<ProductSpecification> _productSpecificationRepository = productSpecificationRepository;
    private readonly ICacheService _cacheService = cacheService;
    public async Task<Result<PagedList<ProductSpecificationGridDTO>>> Handle(GetProductSpecificationGridDTOQuery request, CancellationToken cancellationToken)
    {
        return await _cacheService.GetAsync(request, async () =>
        {
            var query = await _productSpecificationRepository.Query()
            .Where(x => x.ProductId == request.ProductId)
            .Select(x => new ProductSpecificationGridDTO
            {
                Id = x.Id,
                SpecificationAttributeOptionName = x.SpecificationAttributeOption!.Name,
                SpecificationAttributeName = x.SpecificationAttributeOption!.SpecificationAttribute!.Name
            }).ToTableSettings(new PagedListFilterModel()
            {
                PageIndex = request.PageIndex,
                PageSize = request.PageSize,
                FilterModelList = request.FilterModelList,
                OrderByColumnName = request.OrderByColumnName
            });
            return Result.SuccessDataResult(query);
        }, cancellationToken);
    }
}