using eCommerceBase.Domain.SeedWork;

namespace eCommerceBase.Domain.AggregateModels
{
    public class ProductSearch : IElasticEntity
    {
        public Guid Id { get; set; }
        public string? ProductName { get; set; }
    }
}
