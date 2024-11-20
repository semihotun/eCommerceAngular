using MediatR;
using eCommerceBase.Domain.Result;
using eCommerceBase.Domain.AggregateModels;
using eCommerceBase.Persistence.UnitOfWork;
using eCommerceBase.Persistence.GenericRepository;
using eCommerceBase.Insfrastructure.Utilities.Caching.Redis;
using eCommerceBase.Application.Constants;
using eCommerceBase.Application.Handlers.Mapper;

namespace eCommerceBase.Application.Handlers.ShowCaseProducts.Commands;
public record UpdateShowCaseProductCommand(Guid ShowCaseId,
		Guid ProductId,
		System.Guid Id) : IRequest<Result>;
public class UpdateShowCaseProductCommandHandler(IWriteDbRepository<ShowCaseProduct> showCaseProductRepository,
		IUnitOfWork unitOfWork,
		ICacheService cacheService) : IRequestHandler<UpdateShowCaseProductCommand,
		Result>
{
    private readonly IWriteDbRepository<ShowCaseProduct> _showCaseProductRepository = showCaseProductRepository;
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    private readonly ICacheService _cacheService = cacheService;
    public async Task<Result> Handle(UpdateShowCaseProductCommand request,
		CancellationToken cancellationToken)
    {
        return await _unitOfWork.BeginTransaction(async () =>
        {
            var data = await _showCaseProductRepository.GetAsync(u => u.Id == request.Id);
            if (data is not null)
            {
                data = ShowCaseProductMapper.UpdateShowCaseProductCommandToShowCaseProduct(request);
                _showCaseProductRepository.Update(data);
                await _cacheService.RemovePatternAsync("eCommerceBase:ShowCase");
                return Result.SuccessResult(Messages.Updated);
            }

            return Result.ErrorResult(Messages.UpdatedError);
        });
    }
}