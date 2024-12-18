using MediatR;
using eCommerceBase.Domain.Result;
using eCommerceBase.Domain.AggregateModels;
using eCommerceBase.Persistence.UnitOfWork;
using eCommerceBase.Persistence.GenericRepository;
using eCommerceBase.Insfrastructure.Utilities.Caching.Redis;
using eCommerceBase.Application.Constants;

namespace eCommerceBase.Application.Handlers.ProductShipmentInfoes.Commands;
public record DeleteProductShipmentInfoCommand(System.Guid Id) : IRequest<Result>;
public class DeleteProductShipmentInfoCommandHandler(IWriteDbRepository<ProductShipmentInfo> productShipmentInfoRepository,
		IUnitOfWork unitOfWork,
		ICacheService cacheService) : IRequestHandler<DeleteProductShipmentInfoCommand,
		Result>
{
    private readonly IWriteDbRepository<ProductShipmentInfo> _productShipmentInfoRepository = productShipmentInfoRepository;
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    private readonly ICacheService _cacheService = cacheService;
    public async Task<Result> Handle(DeleteProductShipmentInfoCommand request,
		CancellationToken cancellationToken)
    {
        return await _unitOfWork.BeginTransaction(async () =>
        {
            var data = await _productShipmentInfoRepository.GetAsync(p => p.Id == request.Id);
            if (data is not null)
            {
                data.Deleted = true;
                _productShipmentInfoRepository.Update(data);
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