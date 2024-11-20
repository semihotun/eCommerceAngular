using eCommerceBase.Domain.SeedWork;

namespace eCommerceBase.Domain.AggregateModels
{
    public class Currency : BaseEntity
    {
        public string? Symbol { get; private set; }
        public string? Code { get; private set; }
        public string? Name { get; private set; }
        public Currency(string? symbol, string? code, string? name)
        {
            Symbol = symbol;
            Code = code;
            Name = name;
        }
        [SwaggerIgnore]
        public ICollection<ProductStock>? ProductStockList { get; private set; } = [];
    }
}
