using MediatR;
using eCommerceBase.Domain.AggregateModels;
using eCommerceBase.Insfrastructure.Utilities.Grid.PagedList;
using eCommerceBase.Domain.Result;
using eCommerceBase.Application.Handlers.SpecificationAttributeOptions.Queries.Dtos;
using eCommerceBase.Insfrastructure.Utilities.Caching.Redis;
using eCommerceBase.Persistence.Context;
using eCommerceBase.Insfrastructure.Utilities.Grid.Filter;

namespace eCommerceBase.Application.Handlers.SpecificationAttributeOptions.Queries;
public record GetSpecificationAttributeOptionGridDTOQuery(int PageIndex, int PageSize, string? OrderByColumnName, List<FilterModel>? FilterModelList) : IRequest<Result<PagedList<SpecificationAttributeOptionGridDTO>>>;
public class GetSpecificationAttributeOptionGridDTOQueryHandler(CoreDbContext coreDbContext, ICacheService cacheService) : IRequestHandler<GetSpecificationAttributeOptionGridDTOQuery, Result<PagedList<SpecificationAttributeOptionGridDTO>>>
{
    private readonly CoreDbContext _coreDbContext = coreDbContext;
    private readonly ICacheService _cacheService = cacheService;
    public async Task<Result<PagedList<SpecificationAttributeOptionGridDTO>>> Handle(GetSpecificationAttributeOptionGridDTOQuery request, CancellationToken cancellationToken)
    {
        return await _cacheService.GetAsync<Result<PagedList<SpecificationAttributeOptionGridDTO>>>(request, async () =>
        {
            var query = await _coreDbContext.Query<SpecificationAttributeOption>().Select(x => new SpecificationAttributeOptionGridDTO { Id = x.Id, SpecificationAttributeId = x.SpecificationAttributeId, Name = x.Name }).ToTableSettings(new PagedListFilterModel() { PageIndex = request.PageIndex, PageSize = request.PageSize, FilterModelList = request.FilterModelList, OrderByColumnName = request.OrderByColumnName });
            return Result.SuccessDataResult<PagedList<SpecificationAttributeOptionGridDTO>>(query);
        }, cancellationToken);
    }
}