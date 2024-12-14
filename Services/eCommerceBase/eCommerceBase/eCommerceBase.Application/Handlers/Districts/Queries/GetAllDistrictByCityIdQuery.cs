using MediatR;
using eCommerceBase.Domain.Result;
using eCommerceBase.Domain.AggregateModels;
using eCommerceBase.Persistence.GenericRepository;
using eCommerceBase.Insfrastructure.Utilities.Caching.Redis;
using eCommerceBase.Insfrastructure.Utilities.Grid.PagedList;

namespace eCommerceBase.Application.Handlers.States.Queries;
public record GetAllDistrictByCityIdQuery(Guid CityId,int PageIndex,int PageSize,string District) : IRequest<Result<PagedList<District>>>;
public class GetAllDistrictByCityIdQueryHandler(IReadDbRepository<District> districtRepository,
		ICacheService cacheService) : IRequestHandler<GetAllDistrictByCityIdQuery,
		Result<PagedList<District>>>
{
    private readonly IReadDbRepository<District> _districtRepository = districtRepository;
    private readonly ICacheService _cacheService = cacheService;
    public async Task<Result<PagedList<District>>> Handle(GetAllDistrictByCityIdQuery request,
		CancellationToken cancellationToken)
    {
        return await _cacheService.GetAsync(request,
		async () =>
        {
            var data = await _districtRepository.Query()
            .Where(x => request.District.Trim() != ""
                ? x.Name.StartsWith(request.District.Trim()) && x.CityId == request.CityId
                : x.CityId == request.CityId)
            .ToPagedListAsync(request.PageIndex, request.PageSize);
            return Result.SuccessDataResult(data!);
        },
		cancellationToken);
    }
}