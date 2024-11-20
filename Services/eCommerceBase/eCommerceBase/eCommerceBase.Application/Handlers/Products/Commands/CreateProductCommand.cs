using MediatR;
using eCommerceBase.Domain.Result;
using eCommerceBase.Domain.AggregateModels;
using eCommerceBase.Persistence.UnitOfWork;
using eCommerceBase.Persistence.GenericRepository;
using eCommerceBase.Insfrastructure.Utilities.Caching.Redis;
using eCommerceBase.Application.Constants;
using eCommerceBase.Application.Handlers.Mapper;
using eCommerceBase.Application.Helpers;

namespace eCommerceBase.Application.Handlers.Products.Commands;
public record CreateProductCommand(string ProductName,
		Guid? BrandId,
		Guid? CategoryId,
		string ProductContent,
		string Gtin,
		string Sku) : IRequest<Result>;
public class CreateProductCommandHandler(IWriteDbRepository<Product> productRepository,
		IUnitOfWork unitOfWork,
		ICacheService cacheService) : IRequestHandler<CreateProductCommand,
		Result>
{
    private readonly IWriteDbRepository<Product> _productRepository = productRepository;
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    private readonly ICacheService _cacheService = cacheService;
    public async Task<Result> Handle(CreateProductCommand request,
		CancellationToken cancellationToken)
    {
        return await _unitOfWork.BeginTransaction(async () =>
        {
            var data = ProductMapper.CreateProductCommandToProduct(request);
            data.SetSlug(SlugHelper.GenerateSlug(data.ProductName));
            await _productRepository.AddAsync(data);
            await _cacheService.RemovePatternAsync("eCommerceBase:Product");
            return Result.SuccessResult(Messages.Added);
        });
    }
}