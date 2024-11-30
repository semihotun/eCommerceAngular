using eCommerceBase.Application.Handlers.Products.Queries.Dtos;
using eCommerceBase.Domain.AggregateModels;
using System.Linq.Expressions;

namespace eCommerceBase.Application.Handlers.Products.Extenison
{
    public class ProductDetailExtension
    {
        public static Expression<Func<Product, ProductDetailDTO>> ToProductDetailDTO =
            (Product product) => new ProductDetailDTO
            {
                Id = product.Id,
                ProductName = product.ProductName,
                Slug = product.Slug,
                Price = ProductQueryExtensions.CalculatePrice(
                            product.ProductStockList
                                .Where(stock => stock.RemainingStock > 0 && !stock.Deleted)
                                .OrderBy(stock => stock.CreatedOnUtc)
                                .First()
                        ),
                PriceWithoutDiscount = product.ProductStockList
                            .Where(stock => stock.RemainingStock > 0 && !stock.Deleted)
                            .OrderBy(stock => stock.CreatedOnUtc)
                            .First()!.Price,
                PhotoBase64 = product.ProductPhotoList
                            .Where(photo => !photo.Deleted)
                            .First()!.PhotoBase64,
                CurrencyCode = product.ProductStockList
                            .Where(stock => stock.RemainingStock > 0 && !stock.Deleted)
                            .OrderBy(stock => stock.CreatedOnUtc)
                            .First()!.Currency!.Code,
                BrandName = product.Brand!.BrandName,
                CategoryName = product.Category!.CategoryName,
                BrandId = product.BrandId,
                CategoryId = product.CategoryId,
                ProductContent = product.ProductContent
            };
    }
}
