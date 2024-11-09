namespace eCommerceBase.Application.Handlers.ShowCases.Queries.Dtos;
public class ShowcaseGrid
{
    public Guid Id { get; set; }
    public int? ShowCaseOrder { get; set; }
    public string? ShowCaseTitle { get; set; }
    public Guid? ShowCaseTypeId { get; set; }
    public string? ShowCaseText { get; set; }
}