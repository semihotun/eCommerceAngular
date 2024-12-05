using eCommerceBase.Application.Handlers.MailInfos.Commands;
using eCommerceBase.Domain.AggregateModels;
using Riok.Mapperly.Abstractions;

namespace eCommerceBase.Application.Handlers.Mapper
{
    [Mapper]
    public static partial class MailInfoMapper
    {
        public static partial MailInfo UpdateMailInfoCommandToMailInfo(UpdateMailInfoCommand websiteInfo);
    }
}