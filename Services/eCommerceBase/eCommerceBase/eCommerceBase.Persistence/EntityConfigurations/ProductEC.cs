using eCommerceBase.Domain.AggregateModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace eCommerceBase.Persistence.EntityConfigurations
{
    public class ProductEC : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.HasKey(x => x.Id);
            builder.HasIndex(x => x.BrandId);
            builder.HasIndex(x => x.CategoryId);
            builder.HasIndex(x => x.Slug);
            builder.HasIndex(x => x.Deleted);
        }
    }
}
