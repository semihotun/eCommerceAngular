using eCommerceBase.Application.Handlers.Pages.Commands;
using eCommerceBase.Domain.AggregateModels;
using Riok.Mapperly.Abstractions;

namespace eCommerceBase.Application.Handlers.Mapper
{
    [Mapper]
    public static partial class PageMapper
    {
        public static partial Page CreatePageCommandToPage(CreatePageCommand page);
        public static partial Page UpdatePageCommandToPage(UpdatePageCommand page);
    }
}