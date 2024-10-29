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
		IUnitOfWork unitOfWork,
		ICacheService cacheService) : IRequestHandler<DeleteProductCommand,
		Result>
{
    private readonly IWriteDbRepository<Product> _productRepository = productRepository;
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
                await _cacheService.RemovePatternAsync("eCommerceBase:Products");
                return Result.SuccessResult(Messages.Deleted);
            }
            else
            {
                return Result.ErrorResult(Messages.DeletedError);
            }
        });
    }
}