using eCommerceBase.Domain.AggregateModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace eCommerceBase.Persistence.EntityConfigurations
{
    public class CustomerUserEC : IEntityTypeConfiguration<CustomerUser>
    {
        public void Configure(EntityTypeBuilder<CustomerUser> builder)
        {
            builder.HasKey(x => x.Id);
            builder.HasIndex(x => x.UserGroupId);
            builder.HasIndex(x => x.Deleted);
        }
    }
}
