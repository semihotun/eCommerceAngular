using MediatR;
using eCommerceBase.Domain.Result;
using eCommerceBase.Domain.AggregateModels;
using eCommerceBase.Persistence.UnitOfWork;
using eCommerceBase.Persistence.GenericRepository;
using eCommerceBase.Insfrastructure.Utilities.Caching.Redis;
using eCommerceBase.Application.Constants;
using eCommerceBase.Application.Handlers.Mapper;

namespace eCommerceBase.Application.Handlers.ShowCaseProducts.Commands;
public record CreateShowCaseProductCommand(Guid ShowCaseId,
		Guid ProductId) : IRequest<Result>;
public class CreateShowCaseProductCommandHandler(IWriteDbRepository<ShowCaseProduct> showCaseProductRepository,
		IUnitOfWork unitOfWork,
		ICacheService cacheService) : IRequestHandler<CreateShowCaseProductCommand,
		Result>
{
    private readonly IWriteDbRepository<ShowCaseProduct> _showCaseProductRepository = showCaseProductRepository;
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    private readonly ICacheService _cacheService = cacheService;
    public async Task<Result> Handle(CreateShowCaseProductCommand request,
		CancellationToken cancellationToken)
    {
        return await _unitOfWork.BeginTransaction(async () =>
        {
            var data = ShowCaseProductMapper.CreateShowCaseProductCommandToShowCaseProduct(request);
            await _showCaseProductRepository.AddAsync(data);
            await _cacheService.RemovePatternAsync("eCommerceBase:ShowCaseProducts");
            return Result.SuccessResult(Messages.Added);
        });
    }
}