using eCommerceBase.Domain.AggregateModels;
using eCommerceBase.Domain.SeedWork;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace eCommerceBase.Persistence.EntityConfigurations
{
    public class CategoryEC : IEntityTypeConfiguration<Category>,ISeed<Category>
    {
        public void Configure(EntityTypeBuilder<Category> builder)
        {
            builder.HasKey(x => x.Id);

            builder.HasMany(c => c.SubCategoryList)
             .WithOne(c => c.ParentCategory)
             .HasForeignKey(c => c.ParentCategoryId);

            builder.HasIndex(x => x.ParentCategoryId);
            builder.HasIndex(x => x.Deleted);
        }

        public List<Category> GetSeedData()
        {
            var category = new Category("ExemCategory",null, "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Suspendisse cursus magna a tincidunt pharetra.");
            category.SetId(Guid.Parse("d3c1b385-475e-457e-8df5-def8feef2af9"));
            category.SetSlug("exem-category");
            return [category];
        }
    }
}
