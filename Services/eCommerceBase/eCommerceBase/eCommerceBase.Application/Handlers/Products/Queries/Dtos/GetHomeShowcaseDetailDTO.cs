namespace eCommerceBase.Application.Handlers.Products.Queries.Dtos
{
    public class GetHomeShowcaseDetailDTO
    {
        public Guid Id { get; set; }
        public string? ProductName { get; set; }
        public string? BrandName { get; set; }
        public string? Slug { get; set; }
        public string? PhotoBase64 { get; set; }
        public string? CurrencyCode { get; set; }
        public double? Price { get; set; }
        public double? PriceWithoutDiscount { get; set; }
        public double AvgStarRate { get; set; } = 0;
        public int CommentCount { get; set; } = 0;
        public Guid? FavoriteId { get; set; }
    }
}
