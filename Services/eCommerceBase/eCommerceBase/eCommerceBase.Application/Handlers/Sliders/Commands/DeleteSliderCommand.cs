using MediatR;
using eCommerceBase.Domain.Result;
using eCommerceBase.Domain.AggregateModels;
using eCommerceBase.Persistence.UnitOfWork;
using eCommerceBase.Persistence.GenericRepository;
using eCommerceBase.Insfrastructure.Utilities.Caching.Redis;
using eCommerceBase.Application.Constants;

namespace eCommerceBase.Application.Handlers.Sliders.Commands;
public record DeleteSliderCommand(string SliderImage,
		string SliderHeading,
		string SliderText,
		string SliderLink,
		System.Guid Id) : IRequest<Result>;
public class DeleteSliderCommandHandler(IWriteDbRepository<Slider> sliderRepository,
		IUnitOfWork unitOfWork,
		ICacheService cacheService) : IRequestHandler<DeleteSliderCommand,
		Result>
{
    private readonly IWriteDbRepository<Slider> _sliderRepository = sliderRepository;
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    private readonly ICacheService _cacheService = cacheService;
    public async Task<Result> Handle(DeleteSliderCommand request,
		CancellationToken cancellationToken)
    {
        return await _unitOfWork.BeginTransaction(async () =>
        {
            var data = await _sliderRepository.GetAsync(p => p.Id == request.Id);
            if (data is not null)
            {
                data.Deleted = true;
                _sliderRepository.Update(data);
                await _cacheService.RemovePatternAsync("eCommerceBase:Sliders");
                return Result.SuccessResult(Messages.Deleted);
            }
            else
            {
                return Result.ErrorResult(Messages.DeletedError);
            }
        });
    }
}