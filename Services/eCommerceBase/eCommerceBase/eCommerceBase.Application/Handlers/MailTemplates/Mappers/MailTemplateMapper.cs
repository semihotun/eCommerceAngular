using Riok.Mapperly.Abstractions;
using eCommerceBase.Application.Handlers.MailTemplates.Commands;
using eCommerceBase.Domain.AggregateModels;

namespace eCommerceBase.Application.Handlers.Mapper
{
    [Mapper]
    public static partial class MailTemplateMapper
    {
        public static partial void UpdateMailTemplateCommandToMailTemplate(UpdateMailTemplateCommand mailTemplate, MailTemplate mailTemplate);
    }
}