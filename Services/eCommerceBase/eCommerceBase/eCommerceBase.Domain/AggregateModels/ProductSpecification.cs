using eCommerceBase.Domain.SeedWork;

namespace eCommerceBase.Domain.AggregateModels
{
    public class ProductSpecification : BaseEntity
    {
        public Guid ProductId { get; private set; }
        public Guid ProdcutSpecificationOptionId { get; private set; }
        public ProductSpecification(Guid productId, Guid prodcutSpecificationOptionId)
        {
            ProductId = productId;
            ProdcutSpecificationOptionId = prodcutSpecificationOptionId;
        }
        public void SetProduct(Product? product)
        {
            Product = product;
        }
        public void SetSpecificationAttributeOption(SpecificationAttributeOption? specificationAttributeOption)
        {
            SpecificationAttributeOption = specificationAttributeOption;
        }
        [SwaggerIgnore]
        public Product? Product { get; private set; }
        [SwaggerIgnore]
        public SpecificationAttributeOption? SpecificationAttributeOption { get; private set; }
    }
}