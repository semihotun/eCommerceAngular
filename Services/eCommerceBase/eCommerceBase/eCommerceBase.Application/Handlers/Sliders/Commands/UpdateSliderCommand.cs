using MediatR;
using eCommerceBase.Domain.Result;
using eCommerceBase.Domain.AggregateModels;
using eCommerceBase.Persistence.UnitOfWork;
using eCommerceBase.Persistence.GenericRepository;
using eCommerceBase.Insfrastructure.Utilities.Caching.Redis;
using eCommerceBase.Application.Constants;
using eCommerceBase.Application.Handlers.Mapper;
using Microsoft.AspNetCore.Http;
using eCommerceBase.Insfrastructure.Utilities.Extensions;
using MassTransit.Transports;

namespace eCommerceBase.Application.Handlers.Sliders.Commands;
public record UpdateSliderCommand(IFormFile Uploadfile,
		string SliderHeading,
		string SliderText,
		string SliderLink,
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
                SliderMapper.UpdateSliderCommandToSlider(request,data);
                if (request.Uploadfile != null)
                {
                    data.SetSliderImage(request.Uploadfile.ConvertImageToBase64());
                }
                _sliderRepository.Update(data);
                await _cacheService.RemovePatternAsync("eCommerceBase:Sliders");
                return Result.SuccessResult(Messages.Updated);
            }

            return Result.ErrorResult(Messages.UpdatedError);
        });
    }
}