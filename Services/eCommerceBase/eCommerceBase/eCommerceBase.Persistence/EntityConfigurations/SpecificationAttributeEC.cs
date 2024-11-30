using eCommerceBase.Domain.AggregateModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace eCommerceBase.Persistence.EntityConfigurations
{
    public class SpecificationAttributeEC : IEntityTypeConfiguration<SpecificationAttribute>
    {
        public void Configure(EntityTypeBuilder<SpecificationAttribute> builder)
        {
            builder.HasKey(x => x.Id);
            builder.HasIndex(x => x.Deleted);
        }
    }
}
