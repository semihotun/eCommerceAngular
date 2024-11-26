using eCommerceBase.Domain.SeedWork;

namespace eCommerceBase.Domain.AggregateModels
{
    public class DiscountType : BaseEntity
    {
        public DiscountType(string name)
        {
            Name = name;
        }

        public string Name { get; private set; }

        [SwaggerIgnore]
        public ICollection<Discount>? DiscountList { get; private set; } = [];

        public void AddDiscountList(Discount? discount)
        {
            if (discount != null)
            {
                DiscountList?.Add(discount);
            }
        }
    }
}