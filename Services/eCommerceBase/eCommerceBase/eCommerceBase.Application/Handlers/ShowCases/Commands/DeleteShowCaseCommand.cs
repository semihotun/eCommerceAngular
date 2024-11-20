using MediatR;
using eCommerceBase.Domain.Result;
using eCommerceBase.Domain.AggregateModels;
using eCommerceBase.Persistence.UnitOfWork;
using eCommerceBase.Persistence.GenericRepository;
using eCommerceBase.Insfrastructure.Utilities.Caching.Redis;
using eCommerceBase.Application.Constants;
using eCommerceBase.Domain.SeedWork;

namespace eCommerceBase.Application.Handlers.ShowCases.Commands;
public record DeleteShowCaseCommand(System.Guid Id) : IRequest<Result>;
public class DeleteShowCaseCommandHandler(IWriteDbRepository<ShowCase> showCaseRepository,
    IWriteDbRepository<ShowCaseProduct> showCaseProductRepository,
        IUnitOfWork unitOfWork,
        ICacheService cacheService) : IRequestHandler<DeleteShowCaseCommand,
        Result>
{
    private readonly IWriteDbRepository<ShowCase> _showCaseRepository = showCaseRepository;
    private readonly IWriteDbRepository<ShowCaseProduct> _showCaseProductRepository = showCaseProductRepository;
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    private readonly ICacheService _cacheService = cacheService;
    public async Task<Result> Handle(DeleteShowCaseCommand request,
        CancellationToken cancellationToken)
    {
        return await _unitOfWork.BeginTransaction(async () =>
        {
            var data = await _showCaseRepository.GetAsync(p => p.Id == request.Id);
            if (data is not null)
            {
                data.Deleted = true;
                _showCaseRepository.Update(data);
                var showcaseProducts = await _showCaseProductRepository.ToListAsync(x => x.ShowCaseId == data.Id);
                foreach (var item in showcaseProducts)
                {
                    item.Deleted = true;
                    _showCaseProductRepository.Update(item);
                }
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