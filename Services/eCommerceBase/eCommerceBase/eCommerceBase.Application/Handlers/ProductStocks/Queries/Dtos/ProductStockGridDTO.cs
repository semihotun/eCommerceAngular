namespace eCommerceBase.Application.Handlers.ProductStocks.Queries.Dtos;
public class ProductStockGridDTO
{
    public Guid Id { get; set; }
    public int? RemainingStock { get; set; }
    public int? TotalStock { get; set; }
    public string? WarehouseName { get; set; }
}