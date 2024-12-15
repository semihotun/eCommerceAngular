using eCommerceBase.Application.Handlers.ShowCases.Queries.Dtos;
using eCommerceBase.Domain.AggregateModels;
using eCommerceBase.Domain.Result;
using eCommerceBase.Insfrastructure.Utilities.Caching.Redis;
using eCommerceBase.Insfrastructure.Utilities.Grid.Filter;
using eCommerceBase.Insfrastructure.Utilities.Grid.PagedList;
using eCommerceBase.Persistence.GenericRepository;
using MediatR;

namespace eCommerceBase.Application.Handlers.ShowCases.Queries;
public record GetShowcaseGridQuery(int PageIndex, int PageSize, string? OrderByColumnName, List<FilterModel>? FilterModelList) 
    : IRequest<Result<PagedList<ShowcaseGrid>>>;
public class GetShowcaseGridQueryHandler(IReadDbRepository<ShowCase> showcaseRepository, ICacheService cacheService) 
    : IRequestHandler<GetShowcaseGridQuery, Result<PagedList<ShowcaseGrid>>>
{
    private readonly IReadDbRepository<ShowCase> _showcaseRepository = showcaseRepository;
    private readonly ICacheService _cacheService = cacheService;
    public async Task<Result<PagedList<ShowcaseGrid>>> Handle(GetShowcaseGridQuery request, CancellationToken cancellationToken)
    {
        return await _cacheService.GetAsync(request, async () =>
        {
            var query = await _showcaseRepository.Query().Select(x => new ShowcaseGrid
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
            return Result.SuccessDataResult(query);
        }, cancellationToken);
    }
}