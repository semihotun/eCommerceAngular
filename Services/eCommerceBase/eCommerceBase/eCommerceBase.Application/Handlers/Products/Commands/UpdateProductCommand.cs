using MediatR;
using eCommerceBase.Domain.Result;
using eCommerceBase.Domain.AggregateModels;
using eCommerceBase.Persistence.UnitOfWork;
using eCommerceBase.Persistence.GenericRepository;
using eCommerceBase.Insfrastructure.Utilities.Caching.Redis;
using eCommerceBase.Application.Constants;
using eCommerceBase.Application.Handlers.Mapper;
using eCommerceBase.Application.IntegrationEvents.Product;

namespace eCommerceBase.Application.Handlers.Products.Commands;
public record UpdateProductCommand(string ProductName,
		Guid? BrandId,
		Guid? CategoryId,
		string ProductContent,
		string Gtin,
		string Sku,
		System.Guid Id) : IRequest<Result>;
public class UpdateProductCommandHandler(IWriteDbRepository<Product> productRepository,
		IUnitOfWork unitOfWork,
		ICacheService cacheService) : IRequestHandler<UpdateProductCommand,
		Result>
{
    private readonly IWriteDbRepository<Product> _productRepository = productRepository;
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    private readonly ICacheService _cacheService = cacheService;
    public async Task<Result> Handle(UpdateProductCommand request,
		CancellationToken cancellationToken)
    {
        return await _unitOfWork.BeginTransactionAndCreateOutbox(async (setMessage) =>
        {
            var data = await _productRepository.GetAsync(u => u.Id == request.Id);
            if (data is not null)
            {
                data = ProductMapper.UpdateProductCommandToProduct(request);
                data.GenerateSlug();
                _productRepository.Update(data);
                await _cacheService.RemovePatternAsync("eCommerceBase:Product");
                setMessage(new UpdateProductSearchStartedIE(data.Id, data.ProductName));
                return Result.SuccessResult(Messages.Updated);
            }

            return Result.ErrorResult(Messages.UpdatedError);
        });
    }
}