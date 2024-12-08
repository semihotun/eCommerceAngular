using eCommerceBase.Application.Helpers;
using eCommerceBase.Domain.AggregateModels;
using eCommerceBase.Domain.DomainEvents.Products;
using eCommerceBase.Domain.SeedWork;
using eCommerceBase.Persistence.Context;
using eCommerceBase.Persistence.GenericRepository;
using Microsoft.EntityFrameworkCore;

namespace eCommerceBase.Application.DomainEventHandlers.Products
{
    public class ProductSlugGenerateDEHandlers : IObjectNotificationHandler<ProductSlugGenerateDE>
    {
        private readonly CoreDbContext _writeDbContext;
        public ProductSlugGenerateDEHandlers(CoreDbContext writeDbContext)
        {
            _writeDbContext = writeDbContext;
        }
        public async Task<object?> Handle(ProductSlugGenerateDE request, CancellationToken cancellationToken)
        {  
            var product = request.Product;
            var slug = SlugHelper.GenerateSlug(product.ProductName);
            var productSlug =await _writeDbContext.Query<Product>().Where(x=>x.SlugBase == slug).OrderByDescending(x=>x.CreatedOnUtc)
                .Select(x => new { x.SlugBase,x.SlugCounter}).FirstOrDefaultAsync();
            if (productSlug != null)
            {
                product.SetSlug(slug,productSlug.SlugCounter + 1);
            }
            else
            {
                product.SetSlug(slug);
            }
            return Task.FromResult<object?>(true);
        }
    }
}
