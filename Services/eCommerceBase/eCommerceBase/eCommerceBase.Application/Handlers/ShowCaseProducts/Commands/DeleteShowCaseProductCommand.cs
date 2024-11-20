using MediatR;
using eCommerceBase.Domain.Result;
using eCommerceBase.Domain.AggregateModels;
using eCommerceBase.Persistence.UnitOfWork;
using eCommerceBase.Persistence.GenericRepository;
using eCommerceBase.Insfrastructure.Utilities.Caching.Redis;
using eCommerceBase.Application.Constants;

namespace eCommerceBase.Application.Handlers.ShowCaseProducts.Commands;
public record DeleteShowCaseProductCommand(System.Guid Id) : IRequest<Result>;
public class DeleteShowCaseProductCommandHandler(IWriteDbRepository<ShowCaseProduct> showCaseProductRepository,
		IUnitOfWork unitOfWork,
		ICacheService cacheService) : IRequestHandler<DeleteShowCaseProductCommand,
		Result>
{
    private readonly IWriteDbRepository<ShowCaseProduct> _showCaseProductRepository = showCaseProductRepository;
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    private readonly ICacheService _cacheService = cacheService;
    public async Task<Result> Handle(DeleteShowCaseProductCommand request,
		CancellationToken cancellationToken)
    {
        return await _unitOfWork.BeginTransaction(async () =>
        {
            var data = await _showCaseProductRepository.GetAsync(p => p.Id == request.Id);
            if (data is not null)
            {
                data.Deleted = true;
                _showCaseProductRepository.Update(data);
                await _cacheService.RemovePatternAsync("eCommerceBase:ShowCase");
                return Result.SuccessResult(Messages.Deleted);
            }
            else
            {
                return Result.ErrorResult(Messages.DeletedError);
            }
        });
    }
}