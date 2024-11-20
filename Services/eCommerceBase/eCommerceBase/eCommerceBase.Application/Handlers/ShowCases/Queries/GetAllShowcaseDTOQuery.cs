using MediatR;
using eCommerceBase.Domain.AggregateModels;
using eCommerceBase.Insfrastructure.Utilities.Grid.PagedList;
using eCommerceBase.Domain.Result;
using eCommerceBase.Application.Handlers.ShowCases.Queries.Dtos;
using eCommerceBase.Insfrastructure.Utilities.Caching.Redis;
using eCommerceBase.Persistence.Context;
using eCommerceBase.Insfrastructure.Utilities.Grid.Filter;
using Microsoft.EntityFrameworkCore;

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
            var query = await _coreDbContext.Query<ShowCase>()
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
                    ShowCaseProductList = x.ShowCaseProductList.Select(sp => new AllShowcaseDTO.ShowCaseProductDto
                    {
                        Id=sp.Product.Id,
                        ProductName = sp.Product.ProductName,
                        //BrandId = sp.Product.BrandId,
                        //CategoryId = sp.Product.CategoryId,
                        //ProductContent = sp.Product.ProductContent,
                        //Gtin = sp.Product.Gtin,
                        //Sku = sp.Product.Sku,
                        //ProductNameUpper = sp.Product.ProductNameUpper,
                        ProductSeo = sp.Product.ProductSeo,
                        Price = sp.Product.ProductStockList.Where(x => x.RemainingStock > 0 && !x.Deleted).OrderBy(x => x.CreatedOnUtc).FirstOrDefault()!.Price,
                        PhotoBase64 = sp.Product.ProductPhotoList.Where(x=>!x.Deleted).FirstOrDefault()!.PhotoBase64,
                        CurrencyCode= sp.Product.ProductStockList.Where(x => x.RemainingStock > 0 && !x.Deleted).OrderBy(x => x.CreatedOnUtc).FirstOrDefault()!.Currency!.Code
                    }).ToList()
                }).OrderBy(x=>x.ShowCaseOrder).ToListAsync();

            return Result.SuccessDataResult<List<AllShowcaseDTO>>(query!);
        }, cancellationToken);
    }
}