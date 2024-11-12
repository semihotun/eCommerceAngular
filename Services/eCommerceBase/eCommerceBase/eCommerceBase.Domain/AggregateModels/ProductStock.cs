using eCommerceBase.Domain.SeedWork;

namespace eCommerceBase.Domain.AggregateModels
{
    public class ProductStock : BaseEntity
    {
        public int RemainingStock { get; private set; }
        public int TotalStock { get; private set; }
        public Guid WarehouseId { get; private set; }
        public Guid ProductId { get; private set; }

        public ProductStock(int remainingStock, int totalStock, Guid warehouseId, Guid productId)
        {
            RemainingStock = remainingStock;
            TotalStock = totalStock;
            WarehouseId = warehouseId;
            ProductId = productId;
        }
        [SwaggerIgnore]
        public Warehouse? Warehouse { get; set; }
        [SwaggerIgnore]
        public Product? Product { get; set; }
    }
}
