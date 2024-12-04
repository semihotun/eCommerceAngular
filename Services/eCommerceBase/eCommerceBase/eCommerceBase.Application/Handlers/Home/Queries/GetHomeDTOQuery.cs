using eCommerceBase.Application.Handlers.Products.Queries.Dtos;
using eCommerceBase.Application.Handlers.ShowCases.Queries.Dtos;
using eCommerceBase.Application.Handlers.Sliders.Queries;
using eCommerceBase.Domain.AggregateModels;
using eCommerceBase.Domain.Result;
using eCommerceBase.Insfrastructure.Utilities.Caching.Redis;
using eCommerceBase.Insfrastructure.Utilities.Grid.PagedList;
using eCommerceBase.Persistence.Context;
using eCommerceBase.Persistence.EntityConfigurations;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace eCommerceBase.Application.Handlers.ShowCases.Queries;
public record GetHomeDTOQuery()
    : IRequest<Result<HomeDto>>;
public class GetHomeDTOQueryHandler(CoreDbReadContext coreDbContext, ICacheService cacheService,
        ISender sender)
    : IRequestHandler<GetHomeDTOQuery, Result<HomeDto>>
{
    private readonly CoreDbReadContext _coreDbContext = coreDbContext;
    private readonly ISender _sender = sender;
    private readonly ICacheService _cacheService = cacheService;
    public async Task<Result<HomeDto>> Handle(GetHomeDTOQuery request, CancellationToken cancellationToken)
    {
        return await _cacheService.GetAsync<Result<HomeDto>>(request, async () =>
        {
            //Slider'ýda ileride elastiðe taþýyýcam bu yüzden bu þekilde þimdilik
            var sliderQuery = _sender.Send(new GetAllSlider());
            var showcaseQuery = _coreDbContext.Query<ShowCase>()
               .Select(x => new HomeShowcaseDto
               {
                   Id = x.Id,
                   ShowCaseText = x.ShowCaseText,
                   ShowCaseTypeId = x.ShowCaseTypeId,
                   ShowCaseTitle = x.ShowCaseTitle,
                   ShowCaseOrder = x.ShowCaseOrder,
                   ShowCaseProductList = x.ShowCaseProductList
                       .Select(sp => new HomeShowcaseProductDto
                       {
                           Id = sp.Product.Id,
                           ProductName = sp.Product.ProductName,
                           Slug = sp.Product.Slug,
                           Price = ProductQueryExtensions.CalculatePrice(
                               sp.Product.ProductStockList
                               .Where(stock => stock.RemainingStock > 0 && !stock.Deleted)
                               .OrderBy(stock => stock.CreatedOnUtc)
                               .FirstOrDefault()
                           ),
                           PriceWithoutDiscount = sp.Product.ProductStockList
                               .Where(stock => stock.RemainingStock > 0 && !stock.Deleted)
                               .OrderBy(stock => stock.CreatedOnUtc)
                               .First()!.Price,
                           PhotoBase64 = sp.Product.ProductPhotoList
                               .Where(photo => !photo.Deleted)
                               .First()!.PhotoBase64,
                           CurrencyCode = sp.Product.ProductStockList
                               .Where(stock => stock.RemainingStock > 0 && !stock.Deleted)
                               .OrderBy(stock => stock.CreatedOnUtc)
                               .First()!.Currency!.Code,
                       }).Take(10).AsEnumerable()
               })
               .OrderBy(x => x.ShowCaseOrder)
               .AsSplitQuery()
               .ToListAsync();

            await Task.WhenAll(showcaseQuery, sliderQuery);
            var homeDto = new HomeDto
            {
                SliderList = (await sliderQuery).Data,
                ShowcaseList = await showcaseQuery
            };
            return Result.SuccessDataResult<HomeDto>(homeDto);

        }, cancellationToken);
    }
}
// Test gerek sunucuda 
//var showcaseQuery = (
//    from s in _coreDbContext.Query<ShowCase>()
//    orderby s.ShowCaseOrder
//    let ShowCaseProductList = (from sp in _coreDbContext.Query<ShowCaseProduct>()
//                               where sp.ShowCaseId == s.Id
//                               join p in _coreDbContext.Query<Product>() on sp.ProductId equals p.Id
//                               let productPhoto = (from photo in _coreDbContext.Query<ProductPhoto>()
//                                                   where photo.ProductId == p.Id
//                                                   select photo.PhotoBase64).FirstOrDefault()
//                               let productStockList = (from stock in _coreDbContext.Query<ProductStock>()
//                                                       where stock.ProductId == p.Id && stock.RemainingStock > 0
//                                                       orderby stock.CreatedOnUtc
//                                                       let discountProduct = (from discountProduct in _coreDbContext.Query<DiscountProduct>()
//                                                                              where discountProduct.ProductStockId == stock.Id
//                                                                              join discount in _coreDbContext.Query<Discount>() on discountProduct.Id equals discount.Id
//                                                                              select discountProduct).AsEnumerable()
//                                                       select new
//                                                       {
//                                                           Price = discountProduct.Any() ? ProductQueryExtensions.CalculatePrice(stock, discountProduct) : 0,
//                                                           PriceWithoutDiscount = stock.Price,
//                                                           CurrencyCode = stock.Currency!.Code,
//                                                       }).FirstOrDefault()
//                               select new HomeShowcaseProductDto
//                               {
//                                   PhotoBase64 = productPhoto,
//                                   Id = sp.Id,
//                                   ProductName = p.ProductName,
//                                   Slug = p.Slug,
//                                   CurrencyCode = productStockList.CurrencyCode,
//                                   Price = productStockList.Price,
//                                   PriceWithoutDiscount = productStockList.PriceWithoutDiscount,
//                               }).AsEnumerable()
//    select new HomeShowcaseDto
//    {
//        Id = s.Id,
//        ShowCaseText = s.ShowCaseText,
//        ShowCaseTypeId = s.ShowCaseTypeId,
//        ShowCaseTitle = s.ShowCaseTitle,
//        ShowCaseOrder = s.ShowCaseOrder,
//        ShowCaseProductList = ShowCaseProductList
//    }).AsSplitQuery().ToListAsync();