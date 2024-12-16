namespace eCommerceBase.Application.Handlers.Products.Queries.Dtos
{
    public class ProductDetailDTO
    {
        public Guid Id { get; set; }
        public string? ProductName { get; set; }
        public string? Slug { get; set; }
        public string? ImageUrl { get; set; }
        public string? CurrencyCode { get; set; }
        public double? Price { get; set; }
        public double? PriceWithoutDiscount { get; set; }
        public string? BrandName { get; set; }
        public Guid? BrandId { get; set; }
        public string? CategoryName { get; set; }
        public Guid? CategoryId { get; set; }
        public string? ProductContent { get; set; }
        public double AvgStarRate { get; set; } = 0;
        public Guid? FavoriteId { get; set; }
        public int CommentCount { get; set; }
    }
}
