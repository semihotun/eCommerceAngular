using eCommerceBase.Domain.SeedWork;

namespace eCommerceBase.Domain.AggregateModels
{
    public class Discount:BaseEntity
    {
        public Discount(string? name, Guid discountTypeId)
        {
            Name = name;
            DiscountTypeId = discountTypeId;
        }

        public string? Name { get; private set; }
        public Guid DiscountTypeId { get; private set; }
        [SwaggerIgnore]
        public DiscountType? DiscountType { get; private set; }
    }
}
