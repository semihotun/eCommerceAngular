using Riok.Mapperly.Abstractions;
using eCommerceBase.Application.Handlers.WebsiteInfoes.Commands;
using eCommerceBase.Domain.AggregateModels;
using eCommerceBase.Application.Handlers.Mapper;

namespace eCommerceBase.Application.Handlers.Mapper
{
    [Mapper]
    public static partial class WebsiteInfoMapper
    {
        public static partial WebsiteInfo UpdateWebsiteInfoCommandToWebsiteInfo(UpdateWebsiteInfoCommand websiteInfo);
    }
}