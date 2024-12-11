using eCommerceBase.Domain.SeedWork;

namespace eCommerceBase.Domain.AggregateModels
{
    public class ProductComment : BaseEntity
    {
        public Guid ProductId { get; private set; }
        public Guid CustomerUserId { get; private set; }
        public string? Comment { get; private set; }
        public int Rate { get; private set; }
        public bool IsApprove { get; private set; }

        public ProductComment(Guid productId, string? comment, int rate, bool isApprove)
        {
            ProductId = productId;
            Comment = comment;
            Rate = rate;
            IsApprove = isApprove;
        }

        public Product? Product { get; private set; }
        public CustomerUser? CustomerUser { get; private set; }

        public void SetProduct(Product? product)
        {
            Product = product;
        }

        public void SetCustomerUser(CustomerUser? customerUser)
        {
            CustomerUser = customerUser;
        }
        public void SetCustomerUserId(Guid customerUserId)
        {
            CustomerUserId = customerUserId;
        }
        public void ApprovedComment()
        {
            IsApprove = true;
        }
        public void DýsApprovedComment()
        {
            IsApprove = false;
        }
    }
}