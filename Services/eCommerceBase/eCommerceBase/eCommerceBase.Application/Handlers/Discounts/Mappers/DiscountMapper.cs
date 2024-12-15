using eCommerceBase.Application.Handlers.Discounts.Commands;
using eCommerceBase.Domain.AggregateModels;
using Riok.Mapperly.Abstractions;

namespace eCommerceBase.Application.Handlers.Mapper
{
    [Mapper]
    public static partial class DiscountMapper
    {
        public static partial Discount CreateDiscountCommandToDiscount(CreateDiscountCommand discount);
    }
}