using eCommerceBase.Domain.SeedWork;

namespace eCommerceBase.Domain.AggregateModels
{
    public class City : BaseEntity
    {
        public string Name { get; private set; }
        public ICollection<District> DistrictList { get; private set; } = [];

        public City(string name)
        {
            Name = name;
        }

        public void AddDistrictList(District? district)
        {
            if (district != null)
            {
                DistrictList.Add(district);
            }
        }
        public ICollection<CustomerUserAddress> CustomerUserAdressList { get; set; } = [];
    }
}