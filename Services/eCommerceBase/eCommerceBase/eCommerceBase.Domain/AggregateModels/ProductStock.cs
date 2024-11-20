using eCommerceBase.Domain.SeedWork;

namespace eCommerceBase.Domain.AggregateModels
{
    public class ProductStock : BaseEntity
    {
        public int RemainingStock { get; private set; }
        public int TotalStock { get; private set; }
        public Guid WarehouseId { get; private set; }
        public Guid ProductId { get; private set; }
        public double? Price { get; private set; }
        public Guid CurrencyId { get; private set; }

        public ProductStock(int remainingStock, int totalStock, Guid warehouseId, Guid productId, double? price, Guid currencyId)
        {
            RemainingStock = remainingStock;
            TotalStock = totalStock;
            WarehouseId = warehouseId;
            ProductId = productId;
            Price = price;
            CurrencyId = currencyId;
        }
        [SwaggerIgnore]
        public Warehouse? Warehouse { get; set; }
        [SwaggerIgnore]
        public Product? Product { get; set; }
        [SwaggerIgnore]
        public Currency? Currency { get; set; }
    }
}
