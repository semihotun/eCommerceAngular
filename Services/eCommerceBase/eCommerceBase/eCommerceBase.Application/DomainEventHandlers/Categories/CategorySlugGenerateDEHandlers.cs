using eCommerceBase.Application.Helpers;
using eCommerceBase.Domain.AggregateModels;
using eCommerceBase.Domain.DomainEvents.Products;
using eCommerceBase.Domain.SeedWork;
using eCommerceBase.Persistence.Context;
using Microsoft.EntityFrameworkCore;

namespace eCommerceBase.Application.DomainEventHandlers.Products
{
    public class CategorySlugGenerateDEHandlers : IObjectNotificationHandler<CategorySlugGenerateDE>
    {
        private readonly CoreDbContext _writeDbContext;
        public CategorySlugGenerateDEHandlers(CoreDbContext writeDbContext)
        {
            _writeDbContext = writeDbContext;
        }
        public async Task<object?> Handle(CategorySlugGenerateDE request, CancellationToken cancellationToken)
        {  
            var category = request.Category;
            var slug = SlugHelper.GenerateSlug(category.CategoryName);
            var categorySlug =await _writeDbContext.Query<Category>().Where(x=>x.SlugBase == slug).OrderByDescending(x=>x.CreatedOnUtc)
                .Select(x => new { x.SlugBase,x.SlugCounter}).FirstOrDefaultAsync();
            if (categorySlug != null)
            {
                category.SetSlug(slug, categorySlug.SlugCounter + 1);
            }
            else
            {
                category.SetSlug(slug);
            }
            return Task.FromResult<object?>(true);
        }
    }
}
