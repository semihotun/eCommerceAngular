using eCommerceBase.Domain.SeedWork;

namespace eCommerceBase.Domain.AggregateModels
{
    public class MailTemplate:BaseEntity
    {
        public string TemplateHeader { get; private set; }
        public string TemplateContent { get; private set; }
        public ICollection<MailTemplateKeyword> MailTemplateKeywordList { get; private set; } = [];

        public MailTemplate(string templateHeader, string templateContent)
        {
            TemplateHeader = templateHeader;
            TemplateContent = templateContent;
        }
    }
}
