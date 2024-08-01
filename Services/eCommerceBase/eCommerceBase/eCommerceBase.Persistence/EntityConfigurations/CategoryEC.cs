using eCommerceBase.Domain.AggregateModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace eCommerceBase.Persistence.EntityConfigurations
{
    public class CategoryEC : IEntityTypeConfiguration<Category>
    {
        public void Configure(EntityTypeBuilder<Category> builder)
        {
            builder.HasKey(x => x.Id);

            builder.HasMany(c => c.SubCategoryList)
             .WithOne(c => c.ParentCategory)
             .HasForeignKey(c => c.ParentCategoryId)
             .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
