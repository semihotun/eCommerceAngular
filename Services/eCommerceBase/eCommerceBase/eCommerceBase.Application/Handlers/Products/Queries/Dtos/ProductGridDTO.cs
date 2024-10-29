namespace eCommerceBase.Application.Handlers.Products.Queries.Dtos;
public class ProductGridDTO
{
    public Guid Id { get; set; }
    public string? CategoryName { get; set; }
    public string? BrandName { get; set; }
    public string? ProductName { get; set; }
}