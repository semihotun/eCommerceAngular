using eCommerceBase.Domain.AggregateModels;
using eCommerceBase.Domain.SeedWork;

namespace eCommerceBase.Domain.DomainEvents.Products
{
    public class ProductSlugGenerateDE : IObjectNotification
    {
        public Product Product { get; set; }

        public ProductSlugGenerateDE(Product product)
        {
            Product = product;
        }
    }
}
