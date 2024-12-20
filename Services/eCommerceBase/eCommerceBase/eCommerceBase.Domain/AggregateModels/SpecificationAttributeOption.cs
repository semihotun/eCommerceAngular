using eCommerceBase.Domain.SeedWork;

namespace eCommerceBase.Domain.AggregateModels
{
    public class SpecificationAttributeOption : BaseEntity
    {
        public Guid SpecificationAttributeId { get; private set; }
        public string Name { get; private set; }

        public SpecificationAttributeOption(Guid specificationAttributeId, string name)
        {
            SpecificationAttributeId = specificationAttributeId;
            Name = name;
        }
        public void SetSpecificationAttribute(SpecificationAttribute? specificationAttribute)
        {
            SpecificationAttribute = specificationAttribute;
        }
        [SwaggerIgnore]
        public SpecificationAttribute? SpecificationAttribute { get; private set; }

        [SwaggerIgnore]
        public ICollection<ProductSpecification> ProductSpecificationList { get; private set; } = [];
    }
}