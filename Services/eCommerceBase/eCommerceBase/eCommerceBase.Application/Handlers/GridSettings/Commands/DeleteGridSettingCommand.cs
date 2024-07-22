using MediatR;
using eCommerceBase.Domain.Result;
using eCommerceBase.Domain.AggregateModels;
using eCommerceBase.Persistence.UnitOfWork;
using eCommerceBase.Persistence.GenericRepository;
using eCommerceBase.Insfrastructure.Utilities.Caching.Redis;
using eCommerceBase.Application.Constants;

namespace eCommerceBase.Application.Handlers.GridSettings.Commands;
public record DeleteGridSettingCommand(System.Guid Id) : IRequest<Result>;
public class DeleteGridSettingCommandHandler(IWriteDbRepository<GridSetting> gridSettingRepository,
		IUnitOfWork unitOfWork,
		ICacheService cacheService) : IRequestHandler<DeleteGridSettingCommand,
		Result>
{
    private readonly IWriteDbRepository<GridSetting> _gridSettingRepository = gridSettingRepository;
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    private readonly ICacheService _cacheService = cacheService;
    public async Task<Result> Handle(DeleteGridSettingCommand request,
		CancellationToken cancellationToken)
    {
        return await _unitOfWork.BeginTransaction(async () =>
        {
            var data = await _gridSettingRepository.GetAsync(p => p.Id == request.Id);
            if (data is not null)
            {
                data.Deleted = true;
                _gridSettingRepository.Update(data);
                await _cacheService.RemovePatternAsync("eCommerceBase:GridSettings");
                return Result.SuccessResult(Messages.Deleted);
            }
            else
            {
                return Result.ErrorResult(Messages.DeletedError);
            }
        });
    }
}