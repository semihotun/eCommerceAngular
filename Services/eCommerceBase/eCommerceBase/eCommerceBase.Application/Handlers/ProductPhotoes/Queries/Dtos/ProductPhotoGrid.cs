namespace eCommerceBase.Application.Handlers.ProductPhotoes.Queries.Dtos;
public class ProductPhotoGrid
{
    public Guid Id { get; set; }
    public Guid? ProductId { get; set; }
    public string? PhotoBase64 { get; set; }
}