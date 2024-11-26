using MediatR;
using eCommerceBase.Domain.Result;
using eCommerceBase.Domain.AggregateModels;
using eCommerceBase.Persistence.UnitOfWork;
using eCommerceBase.Persistence.GenericRepository;
using eCommerceBase.Insfrastructure.Utilities.Caching.Redis;
using eCommerceBase.Application.Constants;

namespace eCommerceBase.Application.Handlers.DiscountProducts.Commands;
public record DeleteDiscountProductCommand(System.Guid Id) : IRequest<Result>;
public class DeleteDiscountProductCommandHandler(IWriteDbRepository<DiscountProduct> discountProductRepository,
		IUnitOfWork unitOfWork,
		ICacheService cacheService) : IRequestHandler<DeleteDiscountProductCommand,
		Result>
{
    private readonly IWriteDbRepository<DiscountProduct> _discountProductRepository = discountProductRepository;
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    private readonly ICacheService _cacheService = cacheService;
    public async Task<Result> Handle(DeleteDiscountProductCommand request,
		CancellationToken cancellationToken)
    {
        return await _unitOfWork.BeginTransaction(async () =>
        {
            var data = await _discountProductRepository.GetAsync(p => p.Id == request.Id);
            if (data is not null)
            {
                data.Deleted = true;
                _discountProductRepository.Update(data);
                await _cacheService.RemovePatternAsync("eCommerceBase:DiscountProducts");
                return Result.SuccessResult(Messages.Deleted);
            }
            else
            {
                return Result.ErrorResult(Messages.DeletedError);
            }
        });
    }
}