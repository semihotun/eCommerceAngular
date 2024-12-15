using eCommerceBase.Application.Handlers.DiscountProducts.Commands;
using eCommerceBase.Domain.AggregateModels;
using Riok.Mapperly.Abstractions;

namespace eCommerceBase.Application.Handlers.Mapper
{
    [Mapper]
    public static partial class DiscountProductMapper
    {
        public static partial DiscountProduct CreateDiscountProductCommandToDiscountProduct(CreateDiscountProductCommand discountProduct);
        public static partial DiscountProduct UpdateDiscountProductCommandToDiscountProduct(UpdateDiscountProductCommand discountProduct);
    }
}