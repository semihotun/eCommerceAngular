using eCommerceBase.Application.Handlers.Products.Queries.Dtos;
using eCommerceBase.Application.Handlers.ShowCases.Queries.Dtos;
using eCommerceBase.Application.Handlers.Sliders.Queries;
using eCommerceBase.Domain.AggregateModels;
using eCommerceBase.Domain.Constant;
using eCommerceBase.Domain.Result;
using eCommerceBase.Insfrastructure.Utilities.Caching.Redis;
using eCommerceBase.Insfrastructure.Utilities.Grid.PagedList;
using eCommerceBase.Persistence.Context;
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
                           BrandName = sp.Product.Brand!.BrandName,
                           Slug = sp.Product.Slug,
                           Price = sp.Product.ProductStockList
                           .AsQueryable()
                           .Where(stock => stock.RemainingStock > 0 && !stock.Deleted)
                           .OrderBy(stock => stock.CreatedOnUtc)
                           .Select(ProductQueryExtensions.ToCalculatePrice)
                           .FirstOrDefault(),
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
                           CommentCount = sp.Product.ProductCommentList.Count,
                           AvgStarRate = sp.Product.ProductCommentList
                            .Where(x => x.IsApprove)
                            .Select(x => x.Rate)
                            .AsEnumerable()
                            .DefaultIfEmpty()
                            .Average()
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
