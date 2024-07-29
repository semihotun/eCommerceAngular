using MediatR;
using eCommerceBase.Domain.AggregateModels;
using eCommerceBase.Insfrastructure.Utilities.Grid.PagedList;
using eCommerceBase.Domain.Result;
using eCommerceBase.Application.Handlers.SpecificationAttributes.Queries.Dtos;
using eCommerceBase.Insfrastructure.Utilities.Caching.Redis;
using eCommerceBase.Persistence.Context;
using eCommerceBase.Insfrastructure.Utilities.Grid.Filter;

namespace eCommerceBase.Application.Handlers.SpecificationAttributes.Queries;
public record GetSpecificationAttributeGridDTOQuery(int PageIndex, int PageSize, string? OrderByColumnName, List<FilterModel>? FilterModelList) : IRequest<Result<PagedList<SpecificationAttributeGridDTO>>>;
public class GetSpecificationAttributeGridDTOQueryHandler(CoreDbContext coreDbContext, ICacheService cacheService) : IRequestHandler<GetSpecificationAttributeGridDTOQuery, Result<PagedList<SpecificationAttributeGridDTO>>>
{
    private readonly CoreDbContext _coreDbContext = coreDbContext;
    private readonly ICacheService _cacheService = cacheService;
    public async Task<Result<PagedList<SpecificationAttributeGridDTO>>> Handle(GetSpecificationAttributeGridDTOQuery request, CancellationToken cancellationToken)
    {
        return await _cacheService.GetAsync<Result<PagedList<SpecificationAttributeGridDTO>>>(request, async () =>
        {
            var query = await _coreDbContext.Query<SpecificationAttribute>().Select(x => new SpecificationAttributeGridDTO
            {
                Id = x.Id,
                Name = x.Name
            }).ToTableSettings(new PagedListFilterModel()
            {
                PageIndex = request.PageIndex,
                PageSize = request.PageSize,
                FilterModelList = request.FilterModelList,
                OrderByColumnName = request.OrderByColumnName
            });
            return Result.SuccessDataResult<PagedList<SpecificationAttributeGridDTO>>(query);
        }, cancellationToken);
    }
}