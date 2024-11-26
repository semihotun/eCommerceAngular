using eCommerceBase.Domain.AggregateModels;
using eCommerceBase.Domain.Constant;
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
         Price = CalculatePrice(
            product.ProductStockList
             .AsQueryable()
             .Where(stock => stock.RemainingStock > 0 && !stock.Deleted)
             .OrderBy(stock => stock.CreatedOnUtc)
             .FirstOrDefault()
            ),
         PriceWithoutDiscount = product.ProductStockList
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
    private static double CalculatePrice(ProductStock? productStock)
    {
        var totalPrice = productStock!.Price;
        foreach (var item in productStock!.DiscountProductList.Where(x => !x.Deleted))
        {
            if (DiscountTypeConst.ProductCurrencyDiscount == item.Discount!.DiscountTypeId ||
                DiscountTypeConst.CategoryCurrencyDiscount == item.Discount!.DiscountTypeId)
            {
                totalPrice = totalPrice - item.DiscountNumber;
            }
            if (DiscountTypeConst.ProductPercentDiscount == item.Discount!.DiscountTypeId ||
                DiscountTypeConst.CategoryPercentDiscount == item.Discount!.DiscountTypeId)
            {
                totalPrice -= totalPrice * (item.DiscountNumber / 100.0);
            }
        }
        return totalPrice;
    }
}
