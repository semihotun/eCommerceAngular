using eCommerceBase.Domain.AggregateModels;

namespace eCommerceBase.Application.Handlers.MailTemplates.Queries.Dtos;
public class MailTemplateByIdDTO
{
    public Guid Id { get; set; }
    public string? TemplateHeader { get; set; }
    public ICollection<MailTemplateKeyword>? MailTemplateKeywordList { get; set; } = [];
    public string? TemplateContent { get; set; }
}