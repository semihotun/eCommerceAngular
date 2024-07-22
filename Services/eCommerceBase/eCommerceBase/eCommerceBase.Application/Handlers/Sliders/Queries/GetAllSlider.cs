using MediatR;
using eCommerceBase.Domain.Result;
using eCommerceBase.Domain.AggregateModels;
using eCommerceBase.Persistence.GenericRepository;
using eCommerceBase.Insfrastructure.Utilities.Caching.Redis;

namespace eCommerceBase.Application.Handlers.Sliders.Queries;
public record GetAllSlider() : IRequest<Result<IList<Slider>>>;
public class GetAllSliderHandler(IReadDbRepository<Slider> sliderRepository,
		ICacheService cacheService) : IRequestHandler<GetAllSlider,
		Result<IList<Slider>>>
{
    private readonly IReadDbRepository<Slider> _sliderRepository = sliderRepository;
    private readonly ICacheService _cacheService = cacheService;
    public async Task<Result<IList<Slider>>> Handle(GetAllSlider request,
		CancellationToken cancellationToken)
    {
        return await _cacheService.GetAsync(request,
		async () =>
        {
            var data = await _sliderRepository.ToListAsync();
            return Result.SuccessDataResult(data!);
        },
		cancellationToken);
    }
}