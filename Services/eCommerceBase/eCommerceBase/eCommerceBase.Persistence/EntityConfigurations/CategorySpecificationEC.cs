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
        }
    }
}
