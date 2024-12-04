using eCommerceBase.Domain.AggregateModels;
using eCommerceBase.Domain.SeedWork;

namespace eCommerceBase.Application.Handlers.ShowCases.Queries.Dtos;

public class HomeDto
{
    public IList<Slider>? SliderList { get; set; } = [];
    public IEnumerable<HomeShowcaseDto> ShowcaseList { get; set; } = [];
}
public class HomeShowcaseDto
{
    public Guid Id { get; set; }
    public string? ShowCaseText { get; set; }
    public Guid? ShowCaseTypeId { get; set; }
    public string? ShowCaseTitle { get; set; }
    public int? ShowCaseOrder { get; set; }
    public IEnumerable<HomeShowcaseProductDto> ShowCaseProductList { get; set; } = [];
}
public class HomeShowcaseProductDto
{
    public Guid Id { get; set; }
    public string? ProductName { get; set; }
    public string? Slug { get; set; }
    public string? PhotoBase64 { get; set; }
    public string? CurrencyCode { get; set; }
    public double? Price { get; set; }
    public double? PriceWithoutDiscount { get; set; }
}