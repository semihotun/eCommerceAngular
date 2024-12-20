using MediatR;
using eCommerceBase.Domain.Result;
using eCommerceBase.Domain.AggregateModels;
using eCommerceBase.Persistence.UnitOfWork;
using eCommerceBase.Persistence.GenericRepository;
using eCommerceBase.Insfrastructure.Utilities.Caching.Redis;
using eCommerceBase.Application.Constants;
using eCommerceBase.Application.Handlers.Mapper;
using eCommerceBase.Application.Helpers;

namespace eCommerceBase.Application.Handlers.ProductPhotoes.Commands;
public record UpdateProductPhotoCommand(Guid ProductId,
		string ImageUrl,
		System.Guid Id) : IRequest<Result>;
public class UpdateProductPhotoCommandHandler(IWriteDbRepository<ProductPhoto> productPhotoRepository,
		IUnitOfWork unitOfWork,
		ICacheService cacheService) : IRequestHandler<UpdateProductPhotoCommand,
		Result>
{
    private readonly IWriteDbRepository<ProductPhoto> _productPhotoRepository = productPhotoRepository;
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    private readonly ICacheService _cacheService = cacheService;
    public async Task<Result> Handle(UpdateProductPhotoCommand request,
		CancellationToken cancellationToken)
    {
        return await _unitOfWork.BeginTransaction(async () =>
        {
            var data = await _productPhotoRepository.GetAsync(u => u.Id == request.Id);
            if (data is not null)
            {
                data = ProductPhotoMapper.UpdateProductPhotoCommandToProductPhoto(request);
                if (data.ImageUrl != request.ImageUrl)
                {
                    var saveImage = await PhotoHelper.SaveBase64ImageAsWebP(data.Id, data.ImageUrl);
                    if (!saveImage.Success)
                        Result.ErrorResult(saveImage.Message);
                    data.SetImageUrl(saveImage.Data!);
                }
                _productPhotoRepository.Update(data);
                await _cacheService.RemovePatternAsync("eCommerceBase:Product");
                return Result.SuccessResult(Messages.Updated);
            }

            return Result.ErrorResult(Messages.UpdatedError);
        });
    }
}