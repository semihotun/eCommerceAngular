using MediatR;
using eCommerceBase.Domain.Result;
using eCommerceBase.Domain.AggregateModels;
using eCommerceBase.Persistence.GenericRepository;
using eCommerceBase.Insfrastructure.Utilities.Caching.Redis;

namespace eCommerceBase.Application.Handlers.GridSettings.Queries;
public record GetGridSettingByIdQuery(System.Guid Id) : IRequest<Result<GridSetting>>;
public class GetGridSettingByIdQueryHandler(IReadDbRepository<GridSetting> gridSettingRepository,
		ICacheService cacheService) : IRequestHandler<GetGridSettingByIdQuery,
		Result<GridSetting>>
{
    private readonly IReadDbRepository<GridSetting> _gridSettingRepository = gridSettingRepository;
    private readonly ICacheService _cacheService = cacheService;
    public async Task<Result<GridSetting>> Handle(GetGridSettingByIdQuery request,
		CancellationToken cancellationToken)
    {
        return await _cacheService.GetAsync(request,
		async () =>
        {
            var query = await _gridSettingRepository.GetByIdAsync(request.Id);
            return Result.SuccessDataResult<GridSetting>(query!);
        },
		cancellationToken);
    }
}