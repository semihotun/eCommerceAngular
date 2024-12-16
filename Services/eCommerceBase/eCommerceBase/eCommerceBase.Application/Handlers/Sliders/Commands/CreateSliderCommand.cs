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
public record CreateSliderCommand(string SliderHeading,
        string SliderText,
        string SliderLink,
        string ImageUrl) : IRequest<Result>;
public class CreateSliderCommandHandler(IWriteDbRepository<Slider> sliderRepository,
        IUnitOfWork unitOfWork,
        ICacheService cacheService) : IRequestHandler<CreateSliderCommand,
        Result>
{
    private readonly IWriteDbRepository<Slider> _sliderRepository = sliderRepository;
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    private readonly ICacheService _cacheService = cacheService;
    public async Task<Result> Handle(CreateSliderCommand request,
        CancellationToken cancellationToken)
    {
        return await _unitOfWork.BeginTransaction(async () =>
        {
            var id = Guid.NewGuid();
            var data = SliderMapper.CreateSliderCommandToSlider(request);
            data.SetId(id);
            var savePhoto = await PhotoHelper.SaveBase64ImageAsWebP(id, request.ImageUrl);
            if (!savePhoto.Success)
            {
                return Result.SuccessResult(savePhoto.Message);
            }
            data.SetSliderImage(savePhoto.Data!);
            await _sliderRepository.AddAsync(data);
            await _cacheService.RemovePatternAsync("eCommerceBase:Sliders");
            return Result.SuccessResult(Messages.Added);
        });
    }
}