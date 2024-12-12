using eCommerceBase.Application.Handlers.ShowCases.Queries;
using eCommerceBase.Domain.AggregateModels;
using eCommerceBase.Domain.Constant;
using System.Linq.Expressions;
namespace eCommerceBase.Application.Handlers.Products.Queries.Dtos;
public static class ProductQueryExtensions
{
    public static readonly Expression<Func<ProductStock, double?>> ToCalculatePrice = stock =>
            stock.Price - stock.DiscountProductList
                .Where(dp => !dp.Deleted)
                .Sum(dp => dp.Discount!.DiscountTypeId == DiscountTypeConst.ProductCurrencyDiscount
                    ? dp.DiscountNumber
                    : dp.Discount!.DiscountTypeId == DiscountTypeConst.ProductPercentDiscount
                        ? dp.DiscountNumber / 100.0
                        : 0.0);
}
