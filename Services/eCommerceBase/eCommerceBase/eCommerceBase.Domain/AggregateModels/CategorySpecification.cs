using eCommerceBase.Domain.SeedWork;

namespace eCommerceBase.Domain.AggregateModels
{
    public class CategorySpecification : BaseEntity
    {
        public Guid? CategoryId { get; private set; }
        public Guid? SpecificationAttributeteId { get; private set; }

        public CategorySpecification(Guid? categoryId, Guid? specificationAttributeteId)
        {
            CategoryId = categoryId;
            SpecificationAttributeteId = specificationAttributeteId;
        }
        [SwaggerIgnore]
        public Category? Category { get; private set; }
        [SwaggerIgnore]
        public SpecificationAttribute? SpecificationAttribute { get; private set; }

        public void SetCategory(Category? category)
        {
            Category = category;
        }

        public void SetSpecificatioAttribute(SpecificationAttribute? specificationAttribute)
        {
            SpecificationAttribute = specificationAttribute;
        }
    }
}