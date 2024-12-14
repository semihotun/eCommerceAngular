using MediatR;
using eCommerceBase.Domain.Result;
using eCommerceBase.Domain.AggregateModels;
using eCommerceBase.Persistence.GenericRepository;
using eCommerceBase.Insfrastructure.Utilities.Caching.Redis;
using eCommerceBase.Insfrastructure.Utilities.Grid.PagedList;

namespace eCommerceBase.Application.Handlers.Cities.Queries;
public record GetAllCityQuery(int PageIndex, int PageSize,string City="") : IRequest<Result<PagedList<City>>>;
public class GetAllCityQueryHandler(IReadDbRepository<City> cityRepository,
        ICacheService cacheService) : IRequestHandler<GetAllCityQuery,
        Result<PagedList<City>>>
{
    private readonly IReadDbRepository<City> _cityRepository = cityRepository;
    private readonly ICacheService _cacheService = cacheService;
    public async Task<Result<PagedList<City>>> Handle(GetAllCityQuery request,
        CancellationToken cancellationToken)
    {
        return await _cacheService.GetAsync(request,
        async () =>
        {
            var data = await _cityRepository.Query()
               .Where(x => request.City.Trim() != ""
                   ? x.Name.StartsWith(request.City.Trim())
                   : true)
                   .ToPagedListAsync(request.PageIndex, request.PageSize);
            return Result.SuccessDataResult(data!);
        },
        cancellationToken);
    }
}