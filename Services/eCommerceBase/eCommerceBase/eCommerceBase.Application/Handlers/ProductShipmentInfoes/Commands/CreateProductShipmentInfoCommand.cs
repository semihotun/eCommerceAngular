using MediatR;
using eCommerceBase.Domain.Result;
using eCommerceBase.Domain.AggregateModels;
using eCommerceBase.Persistence.UnitOfWork;
using eCommerceBase.Persistence.GenericRepository;
using eCommerceBase.Insfrastructure.Utilities.Caching.Redis;
using eCommerceBase.Application.Constants;
using eCommerceBase.Application.Handlers.Mapper;

namespace eCommerceBase.Application.Handlers.ProductShipmentInfoes.Commands;
public record CreateProductShipmentInfoCommand(double? Width,
		double? Length,
		double? Height,
		double? Weight,
		Guid ProductId) : IRequest<Result<ProductShipmentInfo>>;
public class CreateProductShipmentInfoCommandHandler(IWriteDbRepository<ProductShipmentInfo> productShipmentInfoRepository,
		IUnitOfWork unitOfWork,
		ICacheService cacheService) : IRequestHandler<CreateProductShipmentInfoCommand,
		Result<ProductShipmentInfo>>
{
    private readonly IWriteDbRepository<ProductShipmentInfo> _productShipmentInfoRepository = productShipmentInfoRepository;
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    private readonly ICacheService _cacheService = cacheService;
    public async Task<Result<ProductShipmentInfo>> Handle(CreateProductShipmentInfoCommand request,
		CancellationToken cancellationToken)
    {
        return await _unitOfWork.BeginTransaction(async () =>
        {
            var data = ProductShipmentInfoMapper.CreateProductShipmentInfoCommandToProductShipmentInfo(request);
            await _productShipmentInfoRepository.AddAsync(data);
            await _cacheService.RemovePatternAsync("eCommerceBase:Product");
            return Result.SuccessDataResult(data,Messages.Added);
        });
    }
}