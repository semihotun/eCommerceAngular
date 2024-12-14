using eCommerceBase.Domain.SeedWork;

namespace eCommerceBase.Domain.AggregateModels
{
    public class CustomerUserAddress : BaseEntity
    {
        public string Name { get; private set; }
        public Guid CityId { get; private set; }
        public Guid DistrictId { get; private set; }
        public string Street { get; private set; }
        public string Address { get; private set; }
        public Guid CustomerUserId { get; private set; }

        public CustomerUserAddress(string name, Guid cityId, Guid districtId, string street, string address, Guid customerUserId)
        {
            Name = name;
            CityId = cityId;
            DistrictId = districtId;
            Street = street;
            Address = address;
            CustomerUserId = customerUserId;
        }

        public City? City { get; private set; }
        public District? District { get; private set; }
        public CustomerUser? CustomerUser { get; private set; }

        public void SetCity(City? city)
        {
            City = city;
        }

        public void SetDistrict(District? district)
        {
            District = district;
        }

        public void SetCustomerUser(CustomerUser? customerUser)
        {
            CustomerUser = customerUser;
        }
    }
}