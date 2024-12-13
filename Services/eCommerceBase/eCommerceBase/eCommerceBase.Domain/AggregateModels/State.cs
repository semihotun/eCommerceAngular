using eCommerceBase.Domain.SeedWork;

namespace eCommerceBase.Domain.AggregateModels
{
    public class State : BaseEntity
    {
        public string Name { get; private set; }
        public Guid CityId { get; private set; }

        public State(string name, Guid cityId)
        {
            Name = name;
            CityId = cityId;
        }
        public City? City { get; private set; }
    }
}
