using eCommerceBase.Application.Handlers.Brands.Queries.Dtos;
using eCommerceBase.Domain.AggregateModels;
using eCommerceBase.Domain.Result;
using eCommerceBase.Insfrastructure.Utilities.Caching.Redis;
using eCommerceBase.Insfrastructure.Utilities.Grid.Filter;
using eCommerceBase.Insfrastructure.Utilities.Grid.PagedList;
using eCommerceBase.Persistence.GenericRepository;
using MediatR;

namespace eCommerceBase.Application.Handlers.Brands.Queries;
public record GetBrandGridDTOQuery(int PageIndex, int PageSize, string? OrderByColumnName, List<FilterModel>? FilterModelList) 
    : IRequest<Result<PagedList<BrandGridDTO>>>;
public class GetBrandGridDTOQueryHandler(IReadDbRepository<Brand> brandRepository, ICacheService cacheService) 
    : IRequestHandler<GetBrandGridDTOQuery, Result<PagedList<BrandGridDTO>>>
{
    private readonly IReadDbRepository<Brand> _brandRepository = brandRepository;
    private readonly ICacheService _cacheService = cacheService;
    public async Task<Result<PagedList<BrandGridDTO>>> Handle(GetBrandGridDTOQuery request, CancellationToken cancellationToken)
    {
        return await _cacheService.GetAsync(request, async () =>
        {
            var query = await _brandRepository.Query()
            .Select(x => new BrandGridDTO()
            {
                Id = x.Id,
                BrandName = x.BrandName
            })
            .ToTableSettings(new PagedListFilterModel()
            {
                PageIndex = request.PageIndex,
                PageSize = request.PageSize,
                FilterModelList = request.FilterModelList,
                OrderByColumnName = request.OrderByColumnName
            });
            return Result.SuccessDataResult(query);
        }, cancellationToken);
    }
}