using MediatR;
using eCommerceBase.Domain.AggregateModels;
using eCommerceBase.Insfrastructure.Utilities.Grid.PagedList;
using eCommerceBase.Domain.Result;
using eCommerceBase.Application.Handlers.Pages.Queries.Dtos;
using eCommerceBase.Insfrastructure.Utilities.Caching.Redis;
using eCommerceBase.Persistence.Context;
using eCommerceBase.Insfrastructure.Utilities.Grid.Filter;

namespace eCommerceBase.Application.Handlers.Pages.Queries;
public record GetPageGridDTOQuery(int PageIndex, int PageSize, string? OrderByColumnName, List<FilterModel>? FilterModelList) : IRequest<Result<PagedList<PageGridDTO>>>;
public class GetPageGridDTOQueryHandler(CoreDbContext coreDbContext, ICacheService cacheService) : IRequestHandler<GetPageGridDTOQuery, Result<PagedList<PageGridDTO>>>
{
    private readonly CoreDbContext _coreDbContext = coreDbContext;
    private readonly ICacheService _cacheService = cacheService;
    public async Task<Result<PagedList<PageGridDTO>>> Handle(GetPageGridDTOQuery request, CancellationToken cancellationToken)
    {
        return await _cacheService.GetAsync<Result<PagedList<PageGridDTO>>>(request, async () =>
        {
            var query = await _coreDbContext.Query<Page>().Select(x => new PageGridDTO { Id = x.Id, PageTitle = x.PageTitle }).ToTableSettings(new PagedListFilterModel() { PageIndex = request.PageIndex, PageSize = request.PageSize, FilterModelList = request.FilterModelList, OrderByColumnName = request.OrderByColumnName });
            return Result.SuccessDataResult<PagedList<PageGridDTO>>(query);
        }, cancellationToken);
    }
}