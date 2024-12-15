using eCommerceBase.Application.Handlers.ShowCaseProducts.Commands;
using eCommerceBase.Domain.AggregateModels;
using Riok.Mapperly.Abstractions;

namespace eCommerceBase.Application.Handlers.Mapper
{
    [Mapper]
    public static partial class ShowCaseProductMapper
    {
        public static partial ShowCaseProduct CreateShowCaseProductCommandToShowCaseProduct(CreateShowCaseProductCommand showCaseProduct);
        public static partial ShowCaseProduct UpdateShowCaseProductCommandToShowCaseProduct(UpdateShowCaseProductCommand showCaseProduct);
    }
}