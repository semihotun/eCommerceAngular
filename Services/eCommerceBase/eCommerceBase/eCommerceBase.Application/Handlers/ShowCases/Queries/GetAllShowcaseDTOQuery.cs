using MediatR;
using eCommerceBase.Domain.AggregateModels;
using eCommerceBase.Insfrastructure.Utilities.Grid.PagedList;
using eCommerceBase.Domain.Result;
using eCommerceBase.Application.Handlers.ShowCases.Queries.Dtos;
using eCommerceBase.Insfrastructure.Utilities.Caching.Redis;
using eCommerceBase.Persistence.Context;
using eCommerceBase.Insfrastructure.Utilities.Grid.Filter;
using Microsoft.EntityFrameworkCore;
using eCommerceBase.Application.Handlers.Products.Queries.Dtos;

namespace eCommerceBase.Application.Handlers.ShowCases.Queries;
public record GetAllShowcaseDTOQuery()
    : IRequest<Result<List<AllShowcaseDTO>>>;
public class GetAllShowcaseDTOQueryHandler(CoreDbContext coreDbContext, ICacheService cacheService)
    : IRequestHandler<GetAllShowcaseDTOQuery, Result<List<AllShowcaseDTO>>>
{
    private readonly CoreDbContext _coreDbContext = coreDbContext;
    private readonly ICacheService _cacheService = cacheService;
    public async Task<Result<List<AllShowcaseDTO>>> Handle(GetAllShowcaseDTOQuery request, CancellationToken cancellationToken)
    {
        return await _cacheService.GetAsync<Result<List<AllShowcaseDTO>>>(request, async () =>
        {
            var query = _coreDbContext.Query<ShowCase>()
                    .Include(x => x.ShowCaseProductList)
                        .ThenInclude(x => x.Product)
                        .ThenInclude(x => x.ProductStockList)
                        .ThenInclude(x => x.Currency)
                    .Include(x => x.ShowCaseProductList)
                        .ThenInclude(x => x.Product)
                        .ThenInclude(x => x.ProductPhotoList)
                    .Select(x => new AllShowcaseDTO
                    {
                        Id = x.Id,
                        ShowCaseText = x.ShowCaseText,
                        ShowCaseTypeId = x.ShowCaseTypeId,
                        ShowCaseTitle = x.ShowCaseTitle,
                        ShowCaseOrder = x.ShowCaseOrder,
                        ShowCaseProductList = x.ShowCaseProductList
                            .AsQueryable()
                            .Select(sp => sp.Product)
                            .Select(ProductQueryExtensions.ToProductDto)
                            .AsEnumerable()
                    }).OrderBy(x => x.ShowCaseOrder);
            return Result.SuccessDataResult<List<AllShowcaseDTO>>(await query!.ToListAsync());
        }, cancellationToken);
    }
}