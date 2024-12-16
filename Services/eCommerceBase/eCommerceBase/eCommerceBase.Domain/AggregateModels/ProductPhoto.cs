using eCommerceBase.Domain.SeedWork;

namespace eCommerceBase.Domain.AggregateModels
{
    public class ProductPhoto : BaseEntity
    {
        public Guid ProductId { get; private set; }
        public string ImageUrl { get; private set; }

        public ProductPhoto(Guid productId, string imageUrl)
        {
            ProductId = productId;
            ImageUrl = imageUrl;
        }

        public Product? Product { get; private set; }

        public void SetProduct(Product? product)
        {
            Product = product;
        }
        public void SetImageUrl(string imageUrl)
        {
            ImageUrl = imageUrl;
        }
    }
}