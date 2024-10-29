using MediatR;
using eCommerceBase.Domain.Result;
using eCommerceBase.Domain.AggregateModels;
using eCommerceBase.Persistence.UnitOfWork;
using eCommerceBase.Persistence.GenericRepository;
using eCommerceBase.Insfrastructure.Utilities.Caching.Redis;
using eCommerceBase.Application.Constants;
using eCommerceBase.Application.Handlers.Mapper;

namespace eCommerceBase.Application.Handlers.ProductSpecifications.Commands;
public record CreateProductSpecificationCommand(Guid ProductId,
		Guid ProdcutSpecificationOptionId,
		Product? Product,
		SpecificationAttributeOption? SpecificationAttributeOption) : IRequest<Result>;
public class CreateProductSpecificationCommandHandler(IWriteDbRepository<ProductSpecification> productSpecificationRepository,
		IUnitOfWork unitOfWork,
		ICacheService cacheService) : IRequestHandler<CreateProductSpecificationCommand,
		Result>
{
    private readonly IWriteDbRepository<ProductSpecification> _productSpecificationRepository = productSpecificationRepository;
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    private readonly ICacheService _cacheService = cacheService;
    public async Task<Result> Handle(CreateProductSpecificationCommand request,
		CancellationToken cancellationToken)
    {
        return await _unitOfWork.BeginTransaction(async () =>
        {
            var data = ProductSpecificationMapper.CreateProductSpecificationCommandToProductSpecification(request);
            data.SetProduct(request.Product);
            data.SetSpecificationAttributeOption(request.SpecificationAttributeOption);
            await _productSpecificationRepository.AddAsync(data);
            await _cacheService.RemovePatternAsync("eCommerceBase:ProductSpecifications");
            return Result.SuccessResult(Messages.Added);
        });
    }
}