using eCommerceBase.Application.Handlers.MailTemplates.Commands;
using eCommerceBase.Domain.AggregateModels;
using Riok.Mapperly.Abstractions;

namespace eCommerceBase.Application.Handlers.Mapper
{
    [Mapper]
    public static partial class MailTemplateMapper
    {
        public static partial MailTemplate UpdateMailTemplateCommandToMailTemplate(UpdateMailTemplateCommand mailTemplate);
    }
}