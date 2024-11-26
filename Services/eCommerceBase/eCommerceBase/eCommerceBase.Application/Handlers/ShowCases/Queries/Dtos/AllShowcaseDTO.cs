using eCommerceBase.Application.Handlers.Products.Queries.Dtos;

namespace eCommerceBase.Application.Handlers.ShowCases.Queries.Dtos;
public class AllShowcaseDTO
{
    public Guid Id { get; set; }
    public string? ShowCaseText { get; set; }
    public Guid? ShowCaseTypeId { get; set; }
    public string? ShowCaseTitle { get; set; }
    public int? ShowCaseOrder { get; set; }
    public IEnumerable<ProductDto> ShowCaseProductList { get; set; } = [];
}