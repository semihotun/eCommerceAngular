using MediatR;
using eCommerceBase.Domain.Result;
using eCommerceBase.Domain.AggregateModels;
using eCommerceBase.Persistence.UnitOfWork;
using eCommerceBase.Persistence.GenericRepository;
using eCommerceBase.Insfrastructure.Utilities.Caching.Redis;
using eCommerceBase.Application.Constants;
using eCommerceBase.Application.Handlers.Mapper;

namespace eCommerceBase.Application.Handlers.GridSettings.Commands;
public record CreateGridSettingCommand(string Path,
		string PropertyInfo) : IRequest<Result>;
public class CreateGridSettingCommandHandler(IWriteDbRepository<GridSetting> gridSettingRepository,
		IUnitOfWork unitOfWork,
		ICacheService cacheService) : IRequestHandler<CreateGridSettingCommand,
		Result>
{
    private readonly IWriteDbRepository<GridSetting> _gridSettingRepository = gridSettingRepository;
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    private readonly ICacheService _cacheService = cacheService;
    public async Task<Result> Handle(CreateGridSettingCommand request,
		CancellationToken cancellationToken)
    {
        return await _unitOfWork.BeginTransaction(async () =>
        {
            var data = GridSettingMapper.CreateGridSettingCommandToGridSetting(request);
            await _gridSettingRepository.AddAsync(data);
            await _cacheService.RemovePatternAsync("eCommerceBase:GridSettings");
            return Result.SuccessResult(Messages.Added);
        });
    }
}