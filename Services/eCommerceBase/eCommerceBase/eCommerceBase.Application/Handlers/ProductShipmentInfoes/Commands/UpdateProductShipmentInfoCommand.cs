using MediatR;
using eCommerceBase.Domain.Result;
using eCommerceBase.Domain.AggregateModels;
using eCommerceBase.Persistence.UnitOfWork;
using eCommerceBase.Persistence.GenericRepository;
using eCommerceBase.Insfrastructure.Utilities.Caching.Redis;
using eCommerceBase.Application.Constants;
using eCommerceBase.Application.Handlers.Mapper;

namespace eCommerceBase.Application.Handlers.ProductShipmentInfoes.Commands;
public record UpdateProductShipmentInfoCommand(double? Width,
		double? Length,
		double? Height,
		double? Weight,
		Guid ProductId,
		System.Guid Id) : IRequest<Result>;
public class UpdateProductShipmentInfoCommandHandler(IWriteDbRepository<ProductShipmentInfo> productShipmentInfoRepository,
		IUnitOfWork unitOfWork,
		ICacheService cacheService) : IRequestHandler<UpdateProductShipmentInfoCommand,
		Result>
{
    private readonly IWriteDbRepository<ProductShipmentInfo> _productShipmentInfoRepository = productShipmentInfoRepository;
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    private readonly ICacheService _cacheService = cacheService;
    public async Task<Result> Handle(UpdateProductShipmentInfoCommand request,
		CancellationToken cancellationToken)
    {
        return await _unitOfWork.BeginTransaction(async () =>
        {
            var data = await _productShipmentInfoRepository.GetAsync(u => u.Id == request.Id);
            if (data is not null)
            {
                data = ProductShipmentInfoMapper.UpdateProductShipmentInfoCommandToProductShipmentInfo(request);
                _productShipmentInfoRepository.Update(data);
                await _cacheService.RemovePatternAsync("eCommerceBase:Product");
                return Result.SuccessResult(Messages.Updated);
            }

            return Result.ErrorResult(Messages.UpdatedError);
        });
    }
}