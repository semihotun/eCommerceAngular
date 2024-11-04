using eCommerceBase.Domain.SeedWork;

namespace eCommerceBase.Domain.AggregateModels
{
    public class ProductSpecification : BaseEntity
    {
        public Guid ProductId { get; private set; }
        public Guid SpecificationAttributeOptionId { get; private set; }
        public ProductSpecification(Guid productId, Guid specificationAttributeOptionId)
        {
            ProductId = productId;
            SpecificationAttributeOptionId = specificationAttributeOptionId;
        }
        [SwaggerIgnore]
        public Product? Product { get; private set; }
        [SwaggerIgnore]
        public SpecificationAttributeOption? SpecificationAttributeOption { get; private set; }
    }
}