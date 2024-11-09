using MediatR;
using eCommerceBase.Domain.AggregateModels;
using eCommerceBase.Insfrastructure.Utilities.Grid.PagedList;
using eCommerceBase.Domain.Result;
using eCommerceBase.Application.Handlers.ShowCases.Queries.Dtos;
using eCommerceBase.Insfrastructure.Utilities.Caching.Redis;
using eCommerceBase.Persistence.Context;
using eCommerceBase.Insfrastructure.Utilities.Grid.Filter;

namespace eCommerceBase.Application.Handlers.ShowCases.Queries;
public record GetShowcaseGridQuery(int PageIndex, int PageSize, string? OrderByColumnName, List<FilterModel>? FilterModelList) 
    : IRequest<Result<PagedList<ShowcaseGrid>>>;
public class GetShowcaseGridQueryHandler(CoreDbContext coreDbContext, ICacheService cacheService) 
    : IRequestHandler<GetShowcaseGridQuery, Result<PagedList<ShowcaseGrid>>>
{
    private readonly CoreDbContext _coreDbContext = coreDbContext;
    private readonly ICacheService _cacheService = cacheService;
    public async Task<Result<PagedList<ShowcaseGrid>>> Handle(GetShowcaseGridQuery request, CancellationToken cancellationToken)
    {
        return await _cacheService.GetAsync<Result<PagedList<ShowcaseGrid>>>(request, async () =>
        {
            var query = await _coreDbContext.Query<ShowCase>().Select(x => new ShowcaseGrid
            {
                Id = x.Id,
                ShowCaseOrder = x.ShowCaseOrder,
                ShowCaseTitle = x.ShowCaseTitle,
                ShowCaseTypeId = x.ShowCaseTypeId,
                ShowCaseText = x.ShowCaseText
            }).ToTableSettings(new PagedListFilterModel()
            {
                PageIndex = request.PageIndex,
                PageSize = request.PageSize,
                FilterModelList = request.FilterModelList,
                OrderByColumnName = request.OrderByColumnName
            });
            return Result.SuccessDataResult<PagedList<ShowcaseGrid>>(query);
        }, cancellationToken);
    }
}