using MediatR;
using eCommerceBase.Domain.AggregateModels;
using eCommerceBase.Insfrastructure.Utilities.Grid.PagedList;
using eCommerceBase.Domain.Result;
using eCommerceBase.Application.Handlers.ProductSpecifications.Queries.Dtos;
using eCommerceBase.Insfrastructure.Utilities.Caching.Redis;
using eCommerceBase.Persistence.Context;
using eCommerceBase.Insfrastructure.Utilities.Grid.Filter;
using Microsoft.EntityFrameworkCore;

namespace eCommerceBase.Application.Handlers.ProductSpecifications.Queries;
public record GetProductSpecificationGridDTOQuery(Guid ProductId, int PageIndex, int PageSize, string? OrderByColumnName, List<FilterModel>? FilterModelList)
    : IRequest<Result<PagedList<ProductSpecificationGridDTO>>>;
public class GetProductSpecificationGridDTOQueryHandler(CoreDbContext coreDbContext, ICacheService cacheService)
    : IRequestHandler<GetProductSpecificationGridDTOQuery, Result<PagedList<ProductSpecificationGridDTO>>>
{
    private readonly CoreDbContext _coreDbContext = coreDbContext;
    private readonly ICacheService _cacheService = cacheService;
    public async Task<Result<PagedList<ProductSpecificationGridDTO>>> Handle(GetProductSpecificationGridDTOQuery request, CancellationToken cancellationToken)
    {
        return await _cacheService.GetAsync<Result<PagedList<ProductSpecificationGridDTO>>>(request, async () =>
        {
            var query = await _coreDbContext.Query<ProductSpecification>()
            .Include(x => x.SpecificationAttributeOption)
            .Include(x => x.SpecificationAttributeOption!.SpecificationAttribute)
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
            return Result.SuccessDataResult<PagedList<ProductSpecificationGridDTO>>(query);
        }, cancellationToken);
    }
}