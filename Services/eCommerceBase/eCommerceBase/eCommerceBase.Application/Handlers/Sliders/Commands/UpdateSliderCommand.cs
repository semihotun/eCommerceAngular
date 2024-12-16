using MediatR;
using eCommerceBase.Domain.Result;
using eCommerceBase.Domain.AggregateModels;
using eCommerceBase.Persistence.UnitOfWork;
using eCommerceBase.Persistence.GenericRepository;
using eCommerceBase.Insfrastructure.Utilities.Caching.Redis;
using eCommerceBase.Application.Constants;
using eCommerceBase.Application.Handlers.Mapper;
using eCommerceBase.Application.Helpers;

namespace eCommerceBase.Application.Handlers.Sliders.Commands;
public record UpdateSliderCommand(string SliderHeading,
		string SliderText,
		string SliderLink,
		string ImageUrl,
		System.Guid Id) : IRequest<Result>;
public class UpdateSliderCommandHandler(IWriteDbRepository<Slider> sliderRepository,
		IUnitOfWork unitOfWork,
		ICacheService cacheService) : IRequestHandler<UpdateSliderCommand,
		Result>
{
    private readonly IWriteDbRepository<Slider> _sliderRepository = sliderRepository;
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    private readonly ICacheService _cacheService = cacheService;
    public async Task<Result> Handle(UpdateSliderCommand request,
		CancellationToken cancellationToken)
    {
        return await _unitOfWork.BeginTransaction(async () =>
        {
            var data = await _sliderRepository.GetAsync(u => u.Id == request.Id);
            if (data is not null)
            {      
                data = SliderMapper.UpdateSliderCommandToSlider(request);
                if (data.ImageUrl != request.ImageUrl)
                {
                    var saveImage = await PhotoHelper.SaveBase64ImageAsWebP(data.Id, data.ImageUrl);
                    if (!saveImage.Success)
                        Result.ErrorResult(saveImage.Message);
                    data.SetSliderImage(saveImage.Data!);
                }
                _sliderRepository.Update(data);
                await _cacheService.RemovePatternAsync("eCommerceBase:Sliders");
                return Result.SuccessResult(Messages.Updated);
            }
            return Result.ErrorResult(Messages.UpdatedError);
        });
    }
}