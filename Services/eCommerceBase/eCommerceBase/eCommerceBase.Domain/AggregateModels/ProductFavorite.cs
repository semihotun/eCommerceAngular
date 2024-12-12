using eCommerceBase.Domain.SeedWork;

namespace eCommerceBase.Domain.AggregateModels
{
    public class ProductFavorite : BaseEntity
    {
        public ProductFavorite(Guid productId, Guid customerUserId)
        {
            ProductId = productId;
            CustomerUserId = customerUserId;
        }
        public Guid ProductId { get; private set; }
        public Guid CustomerUserId { get; private set; }
        [SwaggerIgnore]
        public Product? Product {get; private set; }
        [SwaggerIgnore]
        public CustomerUser? CustomerUser { get; private set; }
    }
}
