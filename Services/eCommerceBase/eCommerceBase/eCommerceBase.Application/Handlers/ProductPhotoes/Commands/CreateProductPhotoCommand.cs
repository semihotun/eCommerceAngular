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
public record CreateProductPhotoCommand(Guid ProductId,
		string ImageUrl) : IRequest<Result>;
public class CreateProductPhotoCommandHandler(IWriteDbRepository<ProductPhoto> productPhotoRepository,
		IUnitOfWork unitOfWork,
		ICacheService cacheService) : IRequestHandler<CreateProductPhotoCommand,
		Result>
{
    private readonly IWriteDbRepository<ProductPhoto> _productPhotoRepository = productPhotoRepository;
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    private readonly ICacheService _cacheService = cacheService;
    public async Task<Result> Handle(CreateProductPhotoCommand request,
		CancellationToken cancellationToken)
    {
        return await _unitOfWork.BeginTransaction(async () =>
        {
            var id = Guid.NewGuid();
            var data = ProductPhotoMapper.CreateProductPhotoCommandToProductPhoto(request);
            data.SetId(id);
            var savePhoto = await PhotoHelper.SaveBase64ImageAsWebP(id, request.ImageUrl);
            if (!savePhoto.Success)
            {
                return Result.SuccessResult(savePhoto.Message);
            }
            data.SetImageUrl(savePhoto.Data!);
            await _productPhotoRepository.AddAsync(data);
            await _cacheService.RemovePatternAsync("eCommerceBase:Product");
            return Result.SuccessResult(Messages.Added);
        });
    }
}