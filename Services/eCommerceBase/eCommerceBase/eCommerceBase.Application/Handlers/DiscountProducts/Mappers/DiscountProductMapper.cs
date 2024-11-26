using Riok.Mapperly.Abstractions;
using eCommerceBase.Application.Handlers.DiscountProducts.Commands;
using eCommerceBase.Domain.AggregateModels;
using eCommerceBase.Application.Handlers.Mapper;

namespace eCommerceBase.Application.Handlers.Mapper
{
    [Mapper]
    public static partial class DiscountProductMapper
    {
        public static partial DiscountProduct CreateDiscountProductCommandToDiscountProduct(CreateDiscountProductCommand discountProduct);
        public static partial DiscountProduct UpdateDiscountProductCommandToDiscountProduct(UpdateDiscountProductCommand discountProduct);
    }
}