using eCommerceBase.Application.Handlers.Pages.Queries.Dtos;
using eCommerceBase.Domain.AggregateModels;
using eCommerceBase.Domain.Result;
using eCommerceBase.Insfrastructure.Utilities.Caching.Redis;
using eCommerceBase.Insfrastructure.Utilities.Grid.Filter;
using eCommerceBase.Insfrastructure.Utilities.Grid.PagedList;
using eCommerceBase.Persistence.GenericRepository;
using MediatR;

namespace eCommerceBase.Application.Handlers.Pages.Queries;
public record GetPageGridDTOQuery(int PageIndex, int PageSize, string? OrderByColumnName, List<FilterModel>? FilterModelList)
    : IRequest<Result<PagedList<PageGridDTO>>>;
public class GetPageGridDTOQueryHandler(IReadDbRepository<Page> pageRepository, ICacheService cacheService)
    : IRequestHandler<GetPageGridDTOQuery, Result<PagedList<PageGridDTO>>>
{
    private readonly IReadDbRepository<Page> _pageRepository = pageRepository;
    private readonly ICacheService _cacheService = cacheService;
    public async Task<Result<PagedList<PageGridDTO>>> Handle(GetPageGridDTOQuery request, CancellationToken cancellationToken)
    {
        return await _cacheService.GetAsync(request, async () =>
        {
            var query = await _pageRepository.Query().Select(x => new PageGridDTO
            {
                Id = x.Id,
                PageTitle = x.PageTitle
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