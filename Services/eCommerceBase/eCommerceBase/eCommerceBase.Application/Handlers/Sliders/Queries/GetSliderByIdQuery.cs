using MediatR;
using eCommerceBase.Domain.Result;
using eCommerceBase.Domain.AggregateModels;
using eCommerceBase.Persistence.GenericRepository;
using eCommerceBase.Insfrastructure.Utilities.Caching.Redis;

namespace eCommerceBase.Application.Handlers.Sliders.Queries;
public record GetSliderByIdQuery(Guid Id) : IRequest<Result<Slider>>;
public class GetSliderByIdQueryHandler(IReadDbRepository<Slider> sliderRepository,
		ICacheService cacheService) : IRequestHandler<GetSliderByIdQuery,
		Result<Slider>>
{
    private readonly IReadDbRepository<Slider> _sliderRepository = sliderRepository;
    private readonly ICacheService _cacheService = cacheService;
    public async Task<Result<Slider>> Handle(GetSliderByIdQuery request,
		CancellationToken cancellationToken)
    {
        return await _cacheService.GetAsync(request,
		async () =>
        {
            var query = await _sliderRepository.GetByIdAsync(request.Id);
            return Result.SuccessDataResult<Slider>(query!);
        },
		cancellationToken);
    }
}