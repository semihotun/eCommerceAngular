using eCommerceBase.Domain.SeedWork;

namespace eCommerceBase.Domain.AggregateModels
{
    public class ProductShipmentInfo : BaseEntity, IEntity
    {
        public double? Width { get; set; }
        public double? Length { get; set; }
        public double? Height { get; set; }
        public double? Weight { get; set; }
        public Guid ProductId { get; set; }

        public ProductShipmentInfo(double? width, double? length, double? height, double? weight, Guid productId)
        {
            Width = width;
            Length = length;
            Height = height;
            Weight = weight;
            ProductId = productId;
        }
        [SwaggerIgnore]
        public Product? Product { get; private set; }
    }
}
