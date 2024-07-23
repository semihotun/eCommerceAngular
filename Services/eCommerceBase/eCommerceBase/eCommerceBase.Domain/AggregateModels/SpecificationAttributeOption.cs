using eCommerceBase.Domain.SeedWork;

namespace eCommerceBase.Domain.AggregateModels
{
    public class SpecificationAttributeOption : BaseEntity
    {
        public Guid SpecificationAttributeId { get; private set; }
        public string Name { get; private set; }
        public int DisplayOrder { get; private set; }

        public SpecificationAttributeOption(Guid specificationAttributeId, string name, int displayOrder)
        {
            SpecificationAttributeId = specificationAttributeId;
            Name = name;
            DisplayOrder = displayOrder;
        }

        public SpecificationAttribute? SpecificationAttribute { get; private set; }

        public void SetSpecificationAttribute(SpecificationAttribute? specificationAttribute)
        {
            SpecificationAttribute = specificationAttribute;
        }
    }
}