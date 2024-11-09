using eCommerceBase.Domain.AggregateModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace eCommerceBase.Persistence.EntityConfigurations
{
    public class ShowcaseEC : IEntityTypeConfiguration<ShowCase>
    {
        public void Configure(EntityTypeBuilder<ShowCase> builder)
        {
            builder.HasKey(x => x.Id);
        }
    }
}
