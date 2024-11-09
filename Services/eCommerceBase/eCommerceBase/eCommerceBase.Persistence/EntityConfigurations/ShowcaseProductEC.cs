using eCommerceBase.Domain.AggregateModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace eCommerceBase.Persistence.EntityConfigurations
{
    public class ShowcaseProductEC : IEntityTypeConfiguration<ShowCaseProduct>
    {
        public void Configure(EntityTypeBuilder<ShowCaseProduct> builder)
        {
            builder.HasKey(x => x.Id);
        }
    }
}
