using MediatR;
using eCommerceBase.Domain.Result;
using eCommerceBase.Domain.AggregateModels;
using eCommerceBase.Persistence.UnitOfWork;
using eCommerceBase.Persistence.GenericRepository;
using eCommerceBase.Insfrastructure.Utilities.Caching.Redis;
using eCommerceBase.Application.Constants;

namespace eCommerceBase.Application.Handlers.Products.Commands;
public record DeleteProductCommand(System.Guid Id) : IRequest<Result>;
public class DeleteProductCommandHandler(IWriteDbRepository<Product> productRepository,
        IWriteDbRepository<ProductSpecification> productSpecificationRepository,
        IWriteDbRepository<ProductStock> productStockRepository,
        IWriteDbRepository<ProductPhoto> productPhotoRepository,
        IUnitOfWork unitOfWork,
        ICacheService cacheService) : IRequestHandler<DeleteProductCommand,
        Result>
{
    private readonly IWriteDbRepository<Product> _productRepository = productRepository;
    private readonly IWriteDbRepository<ProductSpecification> _productSpecificationRepository = productSpecificationRepository;
    private readonly IWriteDbRepository<ProductStock> _productStockRepository = productStockRepository;
    private readonly IWriteDbRepository<ProductPhoto> _productPhotoRepository = productPhotoRepository;
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    private readonly ICacheService _cacheService = cacheService;
    public async Task<Result> Handle(DeleteProductCommand request,
        CancellationToken cancellationToken)
    {
        return await _unitOfWork.BeginTransaction(async () =>
        {
            var data = await _productRepository.GetAsync(p => p.Id == request.Id);
            if (data is not null)
            {
                data.Deleted = true;
                _productRepository.Update(data);

                //Delete Product Photo
                var productPhotoList = await _productPhotoRepository.ToListAsync(x => x.ProductId == data.Id);
                foreach(var productPhoto in productPhotoList)
                {
                    productPhoto.Deleted = true;
                    _productPhotoRepository.Update(productPhoto);
                }
                //Delete Product Specification
                var productSpecificationList = await _productSpecificationRepository.ToListAsync(x => x.ProductId == data.Id);
                foreach (var productSpecification in productSpecificationList)
                {
                    productSpecification.Deleted = true;
                    _productSpecificationRepository.Update(productSpecification);
                }
                //Delete Product Stock
                var productStockList = await _productStockRepository.ToListAsync(x => x.ProductId == data.Id);
                foreach (var productStock in productStockList)
                {
                    productStock.Deleted = true;
                    _productStockRepository.Update(productStock);
                }

                await _cacheService.RemovePatternAsync("eCommerceBase:Product");
                return Result.SuccessResult(Messages.Deleted);
            }
            else
            {
                return Result.ErrorResult(Messages.DeletedError);
            }
        });
    }
}