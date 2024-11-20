using MediatR;
using eCommerceBase.Domain.Result;
using eCommerceBase.Domain.AggregateModels;
using eCommerceBase.Persistence.UnitOfWork;
using eCommerceBase.Persistence.GenericRepository;
using eCommerceBase.Insfrastructure.Utilities.Caching.Redis;
using eCommerceBase.Application.Constants;
using eCommerceBase.Application.Handlers.Mapper;

namespace eCommerceBase.Application.Handlers.ProductSpecifications.Commands;
public record UpdateProductSpecificationCommand(Guid ProductId,
		Guid SpecificationAttributeOptionId,
		System.Guid Id) : IRequest<Result>;
public class UpdateProductSpecificationCommandHandler(IWriteDbRepository<ProductSpecification> productSpecificationRepository,
		IUnitOfWork unitOfWork,
		ICacheService cacheService) : IRequestHandler<UpdateProductSpecificationCommand,
		Result>
{
    private readonly IWriteDbRepository<ProductSpecification> _productSpecificationRepository = productSpecificationRepository;
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    private readonly ICacheService _cacheService = cacheService;
    public async Task<Result> Handle(UpdateProductSpecificationCommand request,
		CancellationToken cancellationToken)
    {
        return await _unitOfWork.BeginTransaction(async () =>
        {
            var data = await _productSpecificationRepository.GetAsync(u => u.Id == request.Id);
            if (data is not null)
            {
                data = ProductSpecificationMapper.UpdateProductSpecificationCommandToProductSpecification(request);
                _productSpecificationRepository.Update(data);
                await _cacheService.RemovePatternAsync("eCommerceBase:Product");
                return Result.SuccessResult(Messages.Updated);
            }

            return Result.ErrorResult(Messages.UpdatedError);
        });
    }
}