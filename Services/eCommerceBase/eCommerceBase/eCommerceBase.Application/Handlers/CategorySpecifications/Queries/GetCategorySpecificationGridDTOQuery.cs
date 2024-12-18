using eCommerceBase.Application.Handlers.CategorySpecifications.Queries.Dtos;
using eCommerceBase.Domain.AggregateModels;
using eCommerceBase.Domain.Result;
using eCommerceBase.Insfrastructure.Utilities.Caching.Redis;
using eCommerceBase.Insfrastructure.Utilities.Grid.Filter;
using eCommerceBase.Insfrastructure.Utilities.Grid.PagedList;
using eCommerceBase.Persistence.Context;
using MediatR;

namespace eCommerceBase.Application.Handlers.CategorySpecifications.Queries;
public record GetCategorySpecificationGridDTOQuery(int PageIndex, int PageSize, string? OrderByColumnName, List<FilterModel>? FilterModelList) : IRequest<Result<PagedList<CategorySpecificationGridDTO>>>;
public class GetCategorySpecificationGridDTOQueryHandler(CoreDbContext coreDbContext, ICacheService cacheService) : IRequestHandler<GetCategorySpecificationGridDTOQuery, Result<PagedList<CategorySpecificationGridDTO>>>
{
    private readonly CoreDbContext _coreDbContext = coreDbContext;
    private readonly ICacheService _cacheService = cacheService;
    public async Task<Result<PagedList<CategorySpecificationGridDTO>>> Handle(GetCategorySpecificationGridDTOQuery request, CancellationToken cancellationToken)
    {
        return await _cacheService.GetAsync(request, async () =>
        {
            var query = await _coreDbContext.Query<CategorySpecification>().Select(x => new CategorySpecificationGridDTO
            {
                Id = x.Id,
                SpecificationAttributeName = x.SpecificationAttribute!.Name,
                SpecificationAttributeSpecificationAttributeOptionName = string.Join(',', x.SpecificationAttribute!.SpecificationAttributeOption.Select(s => s.Name))
            })
            .ToTableSettings(new PagedListFilterModel()
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