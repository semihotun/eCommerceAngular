using eCommerceBase.Domain.SeedWork;

namespace eCommerceBase.Domain.AggregateModels
{
    public class SpecificationAttribute : BaseEntity
    {
        public string Name { get; private set; }

        public SpecificationAttribute(string name)
        {
            Name = name;
        }

        [SwaggerIgnore]
        public ICollection<SpecificationAttributeOption> SpecificationAttributeOption { get; private set; } = [];

        [SwaggerIgnore]
        public ICollection<CategorySpecification> CategorySpecificationList { get; private set; } = [];

        public void AddSpecificationAttributeOption(SpecificationAttributeOption? specificationAttributeOption)
        {
            if (specificationAttributeOption != null)
            {
                SpecificationAttributeOption.Add(specificationAttributeOption);
            }
        }

        public void AddCategorySpecificationList(CategorySpecification? categorySpecification)
        {
            if (categorySpecification != null)
            {
                CategorySpecificationList.Add(categorySpecification);
            }
        }
    }
}