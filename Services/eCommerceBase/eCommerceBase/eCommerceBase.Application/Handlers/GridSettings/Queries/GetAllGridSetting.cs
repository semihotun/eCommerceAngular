using MediatR;
using eCommerceBase.Domain.Result;
using eCommerceBase.Domain.AggregateModels;
using eCommerceBase.Persistence.GenericRepository;
using eCommerceBase.Insfrastructure.Utilities.Caching.Redis;

namespace eCommerceBase.Application.Handlers.GridSettings.Queries;
public record GetAllGridSetting() : IRequest<Result<IList<GridSetting>>>;
public class GetAllGridSettingHandler(IReadDbRepository<GridSetting> gridSettingRepository,
		ICacheService cacheService) : IRequestHandler<GetAllGridSetting,
		Result<IList<GridSetting>>>
{
    private readonly IReadDbRepository<GridSetting> _gridSettingRepository = gridSettingRepository;
    private readonly ICacheService _cacheService = cacheService;
    public async Task<Result<IList<GridSetting>>> Handle(GetAllGridSetting request,
		CancellationToken cancellationToken)
    {
        return await _cacheService.GetAsync(request,
		async () =>
        {
            var data = await _gridSettingRepository.ToListAsync();
            return Result.SuccessDataResult(data!);
        },
		cancellationToken);
    }
}