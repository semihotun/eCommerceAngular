using eCommerceBase.Domain.AggregateModels;
using eCommerceBase.Domain.SeedWork;

namespace eCommerceBase.Domain.DomainEvents.Products
{
    public class CategorySlugGenerateDE : IObjectNotification
    {
        public Category Category { get; set; }

        public CategorySlugGenerateDE(Category category)
        {
            Category = category;
        }
    }
}
