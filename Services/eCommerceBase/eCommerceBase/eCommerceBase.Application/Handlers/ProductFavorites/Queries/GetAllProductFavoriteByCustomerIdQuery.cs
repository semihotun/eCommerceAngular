using eCommerceBase.Application.Handlers.Products.Queries.Dtos;
using eCommerceBase.Domain.AggregateModels;
using eCommerceBase.Domain.Result;
using eCommerceBase.Insfrastructure.Utilities.Caching.Redis;
using eCommerceBase.Insfrastructure.Utilities.Grid.PagedList;
using eCommerceBase.Insfrastructure.Utilities.Identity.Middleware;
using eCommerceBase.Persistence.GenericRepository;
using MediatR;

namespace eCommerceBase.Application.Handlers.ProductFavorites.Queries;

public record GetAllProductFavoriteByCustomerIdQuery(int PageIndex, int PageSize) : IRequest<Result<PagedList<ProductFavoriteDTO>>>;
public class GetAllProductFavoriteByCustomerIdQueryHandler(IReadDbRepository<ProductFavorite> productFavoriteRepository,
        ICacheService cacheService, UserScoped userScoped) : IRequestHandler<GetAllProductFavoriteByCustomerIdQuery,
        Result<PagedList<ProductFavoriteDTO>>>
{
    private readonly IReadDbRepository<ProductFavorite> _productFavoriteRepository = productFavoriteRepository;
    private readonly ICacheService _cacheService = cacheService;
    private readonly UserScoped _userScoped = userScoped;
    public async Task<Result<PagedList<ProductFavoriteDTO>>> Handle(GetAllProductFavoriteByCustomerIdQuery request,
        CancellationToken cancellationToken)
    {
        return await _cacheService.GetAsync(request, _userScoped,
        async () =>
        {
            var data =await _productFavoriteRepository.Query()
                        .Where(x => x.CustomerUserId == _userScoped.Id)
                            .Select(sp => new ProductFavoriteDTO
                            {
                                Id = sp.Product!.Id,
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
                                   .Average(),
                                FavoriteId = sp.Id
                            }).ToPagedListAsync(request.PageIndex, request.PageSize);
            return Result.SuccessDataResult(data);
        },
        cancellationToken);
    }
}