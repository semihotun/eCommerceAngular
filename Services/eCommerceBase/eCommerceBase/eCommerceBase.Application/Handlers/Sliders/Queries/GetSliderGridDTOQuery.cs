using MediatR;
using eCommerceBase.Domain.AggregateModels;
using eCommerceBase.Insfrastructure.Utilities.Grid.PagedList;
using eCommerceBase.Domain.Result;
using eCommerceBase.Application.Handlers.Sliders.Queries.Dtos;
using eCommerceBase.Insfrastructure.Utilities.Caching.Redis;
using eCommerceBase.Persistence.Context;
using eCommerceBase.Insfrastructure.Utilities.Grid.Filter;

namespace eCommerceBase.Application.Handlers.Sliders.Queries;
public record GetSliderGridDTOQuery(int PageIndex, int PageSize, string? OrderByColumnName, List<FilterModel>? FilterModelList) : IRequest<Result<PagedList<SliderGridDTO>>>;
public class GetSliderGridDTOQueryHandler(CoreDbContext coreDbContext, ICacheService cacheService) : IRequestHandler<GetSliderGridDTOQuery, Result<PagedList<SliderGridDTO>>>
{
    private readonly CoreDbContext _coreDbContext = coreDbContext;
    private readonly ICacheService _cacheService = cacheService;
    public async Task<Result<PagedList<SliderGridDTO>>> Handle(GetSliderGridDTOQuery request, CancellationToken cancellationToken)
    {
        return await _cacheService.GetAsync<Result<PagedList<SliderGridDTO>>>(request, async () =>
        {
            var query = await _coreDbContext.Query<Slider>().Select(x => new SliderGridDTO { Id = x.Id, SliderHeading = x.SliderHeading, SliderText = x.SliderText, SliderLink = x.SliderLink, SliderImage = x.SliderImage }).ToTableSettings(new PagedListFilterModel() { PageIndex = request.PageIndex, PageSize = request.PageSize, FilterModelList = request.FilterModelList, OrderByColumnName = request.OrderByColumnName });
            return Result.SuccessDataResult<PagedList<SliderGridDTO>>(query);
        }, cancellationToken);
    }
}