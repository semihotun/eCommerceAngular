using eCommerceBase.Domain.AggregateModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace eCommerceBase.Persistence.EntityConfigurations
{
    public class UserGroupRoleEC : IEntityTypeConfiguration<UserGroupRole>
    {
        public void Configure(EntityTypeBuilder<UserGroupRole> builder)
        {
            builder.HasIndex(x => x.Deleted);
            builder.HasIndex(x => x.RoleId);
            builder.HasIndex(x => x.UserGroupId);
            builder.HasKey(x => x.Id);
        }
    }
}
