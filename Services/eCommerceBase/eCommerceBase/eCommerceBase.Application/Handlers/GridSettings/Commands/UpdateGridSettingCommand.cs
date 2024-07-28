using MediatR;
using eCommerceBase.Domain.Result;
using eCommerceBase.Domain.AggregateModels;
using eCommerceBase.Persistence.UnitOfWork;
using eCommerceBase.Persistence.GenericRepository;
using eCommerceBase.Insfrastructure.Utilities.Caching.Redis;
using eCommerceBase.Application.Constants;
using eCommerceBase.Application.Handlers.Mapper;

namespace eCommerceBase.Application.Handlers.GridSettings.Commands;
public record UpdateGridSettingCommand(string Path,
		string PropertyInfo,
		System.Guid? Id) : IRequest<Result>;
public class UpdateGridSettingCommandHandler(IWriteDbRepository<GridSetting> gridSettingRepository,
		IUnitOfWork unitOfWork,
		ICacheService cacheService) : IRequestHandler<UpdateGridSettingCommand,
		Result>
{
    private readonly IWriteDbRepository<GridSetting> _gridSettingRepository = gridSettingRepository;
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    private readonly ICacheService _cacheService = cacheService;
    public async Task<Result> Handle(UpdateGridSettingCommand request,
		CancellationToken cancellationToken)
    {
        return await _unitOfWork.BeginTransaction(async () =>
        {
            var data = await _gridSettingRepository.GetAsync(u => u.Id == request.Id);
            await _cacheService.RemovePatternAsync("eCommerceBase:GridSettings");
            if (data is not null)
            {
                GridSettingMapper.UpdateGridSettingCommandToGridSetting(request,data);
                _gridSettingRepository.Update(data);
                return Result.SuccessResult(Messages.Updated);
            }
            else
            {
                var gridSetting=GridSettingMapper.UpdateGridSettingCommandToNewGridSetting(request);
                await _gridSettingRepository.AddAsync(gridSetting);
                return Result.SuccessResult(Messages.Added);
            }
        });
    }
}