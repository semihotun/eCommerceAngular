using eCommerceBase.Domain.DomainEvents.Products;
using eCommerceBase.Domain.SeedWork;

namespace eCommerceBase.Domain.AggregateModels
{
    public class Category : BaseEntity
    {
        public Category(string categoryName, Guid? parentCategoryId,string categoryDescription)
        {
            CategoryName = categoryName;
            ParentCategoryId = parentCategoryId;
            CategoryDescription = categoryDescription;
        }
        public string CategoryDescription { get; private set; }
        public string CategoryName { get; private set; }
        public Guid? ParentCategoryId { get; private set; }
        public string? SlugBase { get; private set; }
        public string? Slug { get; private set; }
        public int SlugCounter { get; private set; }
        public void SetSlug(string slugBase, int slugCounter = 0)
        {
            SlugBase = slugBase;
            SlugCounter = slugCounter;
            if (slugCounter == 0)
            {
                Slug = slugBase;
            }
            else
            {
                Slug = slugBase + slugCounter;
            }
        }
        public void GenerateSlug()
        {
            base.AddDomainEvent(new CategorySlugGenerateDE(this));
        }
        [SwaggerIgnore]
        public Category? ParentCategory { get; private set; }
        [SwaggerIgnore]
        public ICollection<Category> SubCategoryList { get; private set; } = new List<Category>();
        [SwaggerIgnore]
        public ICollection<CategorySpecification> CategorySpecificationList { get; private set; } = [];
        [SwaggerIgnore]
        public ICollection<Product> ProductList { get; private set; } = [];
    }
}
