using eCommerceBase.Domain.SeedWork;

namespace eCommerceBase.Domain.AggregateModels
{
    public class MailTemplateKeyword : BaseEntity
    {
        public Guid MailTemplateId { get; private set; }
        public string Keyword { get; private set; }
        public string Description { get; private set; }
        public MailTemplate? MailTemplate { get; private set; }
        public MailTemplateKeyword(Guid mailTemplateId, string keyword, string description)
        {
            MailTemplateId = mailTemplateId;
            Keyword = keyword;
            Description = description;
        }
        public void SetMailTemplate(MailTemplate mailTemplate)
        {
            MailTemplate = mailTemplate;
            MailTemplateId = mailTemplate.Id;
        }
    }
}
