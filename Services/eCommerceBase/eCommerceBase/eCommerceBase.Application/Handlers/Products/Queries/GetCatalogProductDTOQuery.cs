using eCommerceBase.Application.Handlers.Products.Extenison;
using eCommerceBase.Application.Handlers.Products.Queries.Dtos;
using eCommerceBase.Domain.AggregateModels;
using eCommerceBase.Domain.Result;
using eCommerceBase.Insfrastructure.Utilities.Caching.Redis;
using eCommerceBase.Insfrastructure.Utilities.Grid.PagedList;
using eCommerceBase.Insfrastructure.Utilities.Identity.Middleware;
using eCommerceBase.Persistence.GenericRepository;
using MediatR;

namespace eCommerceBase.Application.Handlers.Products.Queries
{
    public record GetCatalogProductDTOBySlugQuery(
        string CategorySlug,
        double MaxPrice,
        double MinPrice,
        List<CatalogFilter> Specifications,
        int PageIndex,
        int PageSize
        ) : IRequest<Result<PagedList<GetCatalogProductDTO>>>;
    public class GetCatalogProductDTOQueryHandler(IReadDbRepository<Product> _productRepository,
        UserScoped userScoped, ICacheService cacheService)
        : IRequestHandler<GetCatalogProductDTOBySlugQuery, Result<PagedList<GetCatalogProductDTO>>>
    {
        private readonly IReadDbRepository<Product> _productRepository = _productRepository;
        private readonly UserScoped _userScoped = userScoped;
        private readonly ICacheService _cacheService = cacheService;
        public async Task<Result<PagedList<GetCatalogProductDTO>>> Handle(GetCatalogProductDTOBySlugQuery request, CancellationToken cancellationToken)
        {
            return await _cacheService.GetAsync(request, async () =>
            {
                var specificationExpression = CatalogExpression.SpecificationExpression(request.Specifications);
                var query = _productRepository.Query()
                    .Where(x => x.Category!.Slug == request.CategorySlug)
                          .Where(specificationExpression)
                          .Select(product => new GetCatalogProductDTO
                          {
                              Id = product.Id,
                              ProductName = product.ProductName,
                              Slug = product.Slug,
                              Price = product.ProductStockList
                               .AsQueryable()
                               .Where(stock => stock.RemainingStock > 0 && !stock.Deleted)
                               .OrderBy(stock => stock.CreatedOnUtc)
                               .Select(ProductQueryExtensions.ToCalculatePrice)
                               .FirstOrDefault(),
                              PriceWithoutDiscount = product.ProductStockList
                                .Where(stock => stock.RemainingStock > 0 && !stock.Deleted)
                                .OrderBy(stock => stock.CreatedOnUtc)
                                .First()!.Price,
                              ImageUrl = product.ProductPhotoList
                                .Where(photo => !photo.Deleted)
                                .First()!.ImageUrl,
                              CurrencyCode = product.ProductStockList
                                .Where(stock => stock.RemainingStock > 0 && !stock.Deleted)
                                .OrderBy(stock => stock.CreatedOnUtc)
                                .First()!.Currency!.Code,
                              BrandName = product.Brand!.BrandName,
                              CommentCount = product.ProductCommentList.Count,
                              AvgStarRate = product.ProductCommentList
                                .Where(x => x.IsApprove)
                                .Select(x => x.Rate)
                                .AsEnumerable()
                                .DefaultIfEmpty()
                                .Average(),
                              FavoriteId = _userScoped.Id != Guid.Empty
                                            ? product.ProductFavoriteList
                                             .FirstOrDefault(x => x.CustomerUserId == _userScoped.Id && !x.Deleted)!.Id
                                            : null,

                          })
                          .Where(x => request.MaxPrice > 0
                                    ? x.Price <= request.MaxPrice && x.Price >= request.MinPrice
                                    : x.Price >= request.MinPrice);
                var data = await query.ToPagedListAsync(request.PageIndex, request.PageSize);
                return Result.SuccessDataResult(data);
            },
        cancellationToken);
        }
    }
}

