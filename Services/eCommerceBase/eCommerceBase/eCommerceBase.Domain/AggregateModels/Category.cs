using eCommerceBase.Domain.SeedWork;

namespace eCommerceBase.Domain.AggregateModels
{
    public class Category : BaseEntity
    {
        public Category(string categoryName, Guid? parentCategoryId)
        {
            CategoryName = categoryName;
            ParentCategoryId = parentCategoryId;
        }

        public string CategoryName { get; private set; }
        public Guid? ParentCategoryId { get; private set; }
        [SwaggerIgnore]
        public Category ParentCategory { get; private set; }
        [SwaggerIgnore]
        public ICollection<Category> SubCategoryList { get; private set; } = new List<Category>();
        [SwaggerIgnore]
        public ICollection<CategorySpecification> CategorySpecificationList { get; private set; } = [];
        [SwaggerIgnore]
        public ICollection<Product> ProductList { get; private set; } = [];
    }
}
