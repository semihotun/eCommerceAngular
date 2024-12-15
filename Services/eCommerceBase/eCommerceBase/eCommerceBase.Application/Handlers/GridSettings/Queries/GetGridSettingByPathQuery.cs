using MediatR;
using eCommerceBase.Domain.Result;
using eCommerceBase.Domain.AggregateModels;
using eCommerceBase.Persistence.GenericRepository;
using eCommerceBase.Insfrastructure.Utilities.Caching.Redis;

namespace eCommerceBase.Application.Handlers.GridSettings.Queries;
public record GetGridSettingByPathQuery(string Path) : IRequest<Result<GridSetting>>;
public class GetGridSettingByPathQueryHandler(IReadDbRepository<GridSetting> gridSettingRepository,
		ICacheService cacheService) : IRequestHandler<GetGridSettingByPathQuery,
		Result<GridSetting>>
{
    private readonly IReadDbRepository<GridSetting> _gridSettingRepository = gridSettingRepository;
    private readonly ICacheService _cacheService = cacheService;
    public async Task<Result<GridSetting>> Handle(GetGridSettingByPathQuery request,
		CancellationToken cancellationToken)
    {
        return await _cacheService.GetAsync(request,
		async () =>
        {
            var query = await _gridSettingRepository.GetAsync(x=>x.Path == request.Path);
            return Result.SuccessDataResult(query!);
        },
		cancellationToken);
    }
}