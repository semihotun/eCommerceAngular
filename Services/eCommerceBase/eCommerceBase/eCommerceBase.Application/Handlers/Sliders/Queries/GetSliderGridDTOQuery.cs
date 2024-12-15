using eCommerceBase.Application.Handlers.Sliders.Queries.Dtos;
using eCommerceBase.Domain.AggregateModels;
using eCommerceBase.Domain.Result;
using eCommerceBase.Insfrastructure.Utilities.Caching.Redis;
using eCommerceBase.Insfrastructure.Utilities.Grid.Filter;
using eCommerceBase.Insfrastructure.Utilities.Grid.PagedList;
using eCommerceBase.Persistence.GenericRepository;
using MediatR;

namespace eCommerceBase.Application.Handlers.Sliders.Queries;
public record GetSliderGridDTOQuery(int PageIndex, int PageSize, string? OrderByColumnName, List<FilterModel>? FilterModelList)
    : IRequest<Result<PagedList<SliderGridDTO>>>;
public class GetSliderGridDTOQueryHandler(IReadDbRepository<Slider> sliderRepository, ICacheService cacheService)
    : IRequestHandler<GetSliderGridDTOQuery, Result<PagedList<SliderGridDTO>>>
{
    private readonly IReadDbRepository<Slider> _sliderRepository = sliderRepository;
    private readonly ICacheService _cacheService = cacheService;
    public async Task<Result<PagedList<SliderGridDTO>>> Handle(GetSliderGridDTOQuery request, CancellationToken cancellationToken)
    {
        return await _cacheService.GetAsync(request, async () =>
        {
            var query = await _sliderRepository.Query().Select(x => new SliderGridDTO
            {
                Id = x.Id,
                SliderHeading = x.SliderHeading,
                SliderText = x.SliderText,
                SliderLink = x.SliderLink,
                SliderImage = x.SliderImage
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