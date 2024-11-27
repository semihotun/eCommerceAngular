using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eCommerceBase.Application.Handlers.Products.Queries.Dtos
{
    public class ProductDto
    {
        public Guid Id { get; set; }
        public string? ProductName { get; set; }
        public string? Slug { get; set; }
        public string? PhotoBase64 { get; set; }
        public string? CurrencyCode { get; set; }
        public double? Price { get; set; }
        public double? PriceWithoutDiscount { get; set; }
    }
}
