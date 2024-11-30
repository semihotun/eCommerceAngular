using eCommerceBase.Domain.AggregateModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace eCommerceBase.Persistence.EntityConfigurations
{
    public class ProductSpecificationEC : IEntityTypeConfiguration<ProductSpecification>
    {
        public void Configure(EntityTypeBuilder<ProductSpecification> builder)
        {
            builder.HasKey(x => x.Id);
            builder.HasIndex(x => x.ProductId);
            builder.HasIndex(x => x.SpecificationAttributeOptionId);
            builder.HasIndex(x => x.Deleted);
        }
    }
}
