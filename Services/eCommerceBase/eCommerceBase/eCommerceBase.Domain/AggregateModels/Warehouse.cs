using eCommerceBase.Domain.SeedWork;

namespace eCommerceBase.Domain.AggregateModels
{
    public class Warehouse : BaseEntity
    {
        public string Name { get; private set; }
        public string Address { get; private set; }

        public Warehouse(string name, string address)
        {
            Name = name;
            Address = address;
        }
    }
}
