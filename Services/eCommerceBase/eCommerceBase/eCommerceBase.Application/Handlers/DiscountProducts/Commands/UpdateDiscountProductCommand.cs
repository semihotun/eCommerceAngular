using MediatR;
using eCommerceBase.Domain.Result;
using eCommerceBase.Domain.AggregateModels;
using eCommerceBase.Persistence.UnitOfWork;
using eCommerceBase.Persistence.GenericRepository;
using eCommerceBase.Insfrastructure.Utilities.Caching.Redis;
using eCommerceBase.Application.Constants;
using eCommerceBase.Application.Handlers.Mapper;

namespace eCommerceBase.Application.Handlers.DiscountProducts.Commands;
public record UpdateDiscountProductCommand(Guid DiscountId,
		Guid ProductStockId,
		int DiscountNumber,
		ProductStock? ProductStock,
		Discount? Discount,
		System.Guid Id) : IRequest<Result>;
public class UpdateDiscountProductCommandHandler(IWriteDbRepository<DiscountProduct> discountProductRepository,
		IUnitOfWork unitOfWork,
		ICacheService cacheService) : IRequestHandler<UpdateDiscountProductCommand,
		Result>
{
    private readonly IWriteDbRepository<DiscountProduct> _discountProductRepository = discountProductRepository;
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    private readonly ICacheService _cacheService = cacheService;
    public async Task<Result> Handle(UpdateDiscountProductCommand request,
		CancellationToken cancellationToken)
    {
        return await _unitOfWork.BeginTransaction(async () =>
        {
            var data = await _discountProductRepository.GetAsync(u => u.Id == request.Id);
            if (data is not null)
            {
                data = DiscountProductMapper.UpdateDiscountProductCommandToDiscountProduct(request);
                _discountProductRepository.Update(data);
                await _cacheService.RemovePatternAsync("eCommerceBase:DiscountProducts");
                return Result.SuccessResult(Messages.Updated);
            }

            return Result.ErrorResult(Messages.UpdatedError);
        });
    }
}