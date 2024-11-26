using Riok.Mapperly.Abstractions;
using eCommerceBase.Application.Handlers.Discounts.Commands;
using eCommerceBase.Domain.AggregateModels;
using eCommerceBase.Application.Handlers.Mapper;

namespace eCommerceBase.Application.Handlers.Mapper
{
    [Mapper]
    public static partial class DiscountMapper
    {
        public static partial Discount CreateDiscountCommandToDiscount(CreateDiscountCommand discount);
    }
}