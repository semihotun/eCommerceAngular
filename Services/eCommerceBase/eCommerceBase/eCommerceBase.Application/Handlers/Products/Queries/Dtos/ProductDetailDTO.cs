﻿namespace eCommerceBase.Application.Handlers.Products.Queries.Dtos
{
    public class ProductDetailDTO
    {
        public Guid Id { get; set; }
        public string? ProductName { get; set; }
        public string? Slug { get; set; }
        public string? PhotoBase64 { get; set; }
        public string? CurrencyCode { get; set; }
        public double? Price { get; set; }
        public double? PriceWithoutDiscount { get; set; }
        public string? BrandName { get; set; }
        public Guid? BrandId { get; set; }
        public string? CategoryName { get; set; }
        public Guid? CategoryId { get; set; }
        public string? ProductContent { get; set; }
    }
}