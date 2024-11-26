using eCommerceBase.Domain.AggregateModels;
using System.Linq.Expressions;
namespace eCommerceBase.Application.Handlers.Products.Queries.Dtos;
public static class ProductQueryExtensions
{
    public static Expression<Func<Product, ProductDto>> ToProductDto =>
     (Product product) => new ProductDto
     {
         Id = product.Id,
         ProductName = product.ProductName,
         ProductSeo = product.ProductSeo,
         Price = product.ProductStockList
             .AsQueryable()
             .Where(stock => stock.RemainingStock > 0 && !stock.Deleted)
             .OrderBy(stock => stock.CreatedOnUtc)
             .FirstOrDefault()!.Price,
         PhotoBase64 = product.ProductPhotoList
             .AsQueryable()
             .Where(photo => !photo.Deleted)
             .FirstOrDefault()!.PhotoBase64,
         CurrencyCode = product.ProductStockList
             .AsQueryable()
             .Where(stock => stock.RemainingStock > 0 && !stock.Deleted)
             .OrderBy(stock => stock.CreatedOnUtc)
             .FirstOrDefault()!.Currency!.Code
     };
}
