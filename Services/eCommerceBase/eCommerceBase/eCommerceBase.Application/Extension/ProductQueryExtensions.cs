using eCommerceBase.Domain.AggregateModels;
using eCommerceBase.Domain.Constant;
using System.Linq.Expressions;
namespace eCommerceBase.Application.Handlers.Products.Queries.Dtos;
public static class ProductQueryExtensions
{
    public static double CalculatePrice(ProductStock? productStock)
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
    public static Expression<Func<ProductStock, double>> CalculatePriceExpression()
    {
        return productStock => productStock.Price -
               productStock.DiscountProductList
                   .Where(x => !x.Deleted)
                   .Aggregate(
                       0.0,
                       (current, item) =>
                           current +
                           (item.Discount!.DiscountTypeId == DiscountTypeConst.ProductCurrencyDiscount ||
                            item.Discount.DiscountTypeId == DiscountTypeConst.CategoryCurrencyDiscount
                               ? item.DiscountNumber
                               : item.Discount.DiscountTypeId == DiscountTypeConst.ProductPercentDiscount ||
                                 item.Discount.DiscountTypeId == DiscountTypeConst.CategoryPercentDiscount
                                   ? productStock.Price * (item.DiscountNumber / 100.0)
                                   : 0));
    }
}
