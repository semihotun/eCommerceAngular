using eCommerceBase.Domain.AggregateModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace eCommerceBase.Persistence.EntityConfigurations
{
    public class CategorySpecificationEC : IEntityTypeConfiguration<CategorySpecification>
    {
        public void Configure(EntityTypeBuilder<CategorySpecification> builder)
        {
            builder.HasKey(x => x.Id);
            builder.HasIndex(x => x.CategoryId);
            builder.HasIndex(x => x.SpecificationAttributeteId);
            builder.HasIndex(x => x.Deleted);

            builder.HasOne(x => x.SpecificationAttribute) 
                 .WithMany(x => x.CategorySpecificationList) 
                 .HasForeignKey(x => x.SpecificationAttributeteId);

        }
    }
}
