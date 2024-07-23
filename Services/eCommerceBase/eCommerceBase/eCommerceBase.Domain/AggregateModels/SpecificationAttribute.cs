using eCommerceBase.Domain.SeedWork;

namespace eCommerceBase.Domain.AggregateModels
{
    public class SpecificationAttribute : BaseEntity
    {
        public string Name { get; private set; }
        public int DisplayOrder { get; private set; }

        public SpecificationAttribute(string name, int displayOrder)
        {
            Name = name;
            DisplayOrder = displayOrder;
        }
        public ICollection<SpecificationAttributeOption> SpecificationAttributeOption { get; private set; } = [];

        public void AddSpecificationAttributeOption(SpecificationAttributeOption? specificationAttributeOption)
        {
            if (specificationAttributeOption != null)
            {
                SpecificationAttributeOption.Add(specificationAttributeOption);
            }
        }
    }
}