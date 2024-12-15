using eCommerceBase.Application.Handlers.WebsiteInfoes.Commands;
using eCommerceBase.Domain.AggregateModels;
using Riok.Mapperly.Abstractions;

namespace eCommerceBase.Application.Handlers.Mapper
{
    [Mapper]
    public static partial class WebsiteInfoMapper
    {
        public static partial WebsiteInfo UpdateWebsiteInfoCommandToWebsiteInfo(UpdateWebsiteInfoCommand websiteInfo);
    }
}