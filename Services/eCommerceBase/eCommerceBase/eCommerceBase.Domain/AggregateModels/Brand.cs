using eCommerceBase.Domain.SeedWork;

namespace eCommerceBase.Domain.AggregateModels
{
    public class Brand:BaseEntity
    {
        public string BrandName { get; set; }

        public Brand(string brandName)
        {
            BrandName = brandName;
        }
    }
}
