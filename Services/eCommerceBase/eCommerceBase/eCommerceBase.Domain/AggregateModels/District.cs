using eCommerceBase.Domain.SeedWork;

namespace eCommerceBase.Domain.AggregateModels
{
    public class District : BaseEntity
    {
        public string Name { get; private set; }
        public Guid CityId { get; private set; }

        public District(string name, Guid cityId)
        {
            Name = name;
            CityId = cityId;
        }
        public City? City { get; private set; }
        public ICollection<CustomerUserAddress> CustomerUserAdressList { get; set; } = [];
    }
}
