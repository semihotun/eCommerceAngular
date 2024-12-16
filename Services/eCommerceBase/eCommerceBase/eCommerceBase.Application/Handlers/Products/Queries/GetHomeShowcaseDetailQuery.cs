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
    public record GetHomeShowcaseDetailQuery(Guid ShowCaseId, int PageIndex, int PageSize) : IRequest<Result<PagedList<GetHomeShowcaseDetailDTO>>>;

    public class GetHomeShowcaseDetailQueryHandler(IReadDbRepository<ShowCaseProduct> showcaseProductRepository, ICacheService cacheService,
        UserScoped userScoped) : IRequestHandler<GetHomeShowcaseDetailQuery, Result<PagedList<GetHomeShowcaseDetailDTO>>>
    {
        private readonly ICacheService _cacheService = cacheService;
        private readonly UserScoped _userScoped = userScoped;
        private readonly IReadDbRepository<ShowCaseProduct> _showcaseProductRepository = showcaseProductRepository;

        public async Task<Result<PagedList<GetHomeShowcaseDetailDTO>>> Handle(GetHomeShowcaseDetailQuery request, CancellationToken cancellationToken)
        {
            return await _cacheService.GetAsync(request, _userScoped, async () =>
            {
                var data = await _showcaseProductRepository.Query()
                       .Where(x => x.ShowCaseId == request.ShowCaseId)
                       .Select(sp => new GetHomeShowcaseDetailDTO
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
                           ImageUrl = sp.Product.ProductPhotoList
                               .Where(photo => !photo.Deleted)
                               .First()!.ImageUrl,
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
                           FavoriteId = _userScoped!.Id != Guid.Empty
                                        ? sp.Product.ProductFavoriteList
                                         .FirstOrDefault(x => x.CustomerUserId == _userScoped.Id && !x.Deleted)!.Id
                                        : null

                       }).ToPagedListAsync(request.PageIndex, request.PageSize);

                return Result.SuccessDataResult(data!);
            }, cancellationToken);
        }
    }
}
