using eCommerceBase.Domain.SeedWork;

namespace eCommerceBase.Domain.AggregateModels
{
    public class City : BaseEntity
    {
        public string Name { get; private set; }
        public ICollection<State> StateList { get; private set; } = [];

        public City(string name)
        {
            Name = name;
        }

        public void AddStateList(State? state)
        {
            if (state != null)
            {
                StateList.Add(state);
            }
        }
    }
}