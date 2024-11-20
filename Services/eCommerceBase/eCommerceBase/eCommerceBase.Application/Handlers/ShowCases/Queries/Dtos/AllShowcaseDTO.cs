using eCommerceBase.Domain.AggregateModels;
using eCommerceBase.Domain.SeedWork;

namespace eCommerceBase.Application.Handlers.ShowCases.Queries.Dtos;
public class AllShowcaseDTO
{
    public Guid Id { get; set; }
    public string? ShowCaseText { get; set; }
    public Guid? ShowCaseTypeId { get; set; }
    public string? ShowCaseTitle { get; set; }
    public int? ShowCaseOrder { get; set; }
    public IList<ShowCaseProductDto> ShowCaseProductList { get; set; } = [];
    public class ShowCaseProductDto 
    {
        public Guid Id { get; set; }
        public string? ProductName { get; set; }
        public string? ProductSeo { get; set; }
        public string? PhotoBase64 { get; set; }
        public string? CurrencyCode { get; set; }
        public double? Price { get; set; }
    }
}