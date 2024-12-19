using eCommerceBase.Domain.AggregateModels;
using eCommerceBase.Domain.Constant;
using eCommerceBase.Domain.SeedWork;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace eCommerceBase.Persistence.EntityConfigurations
{
    public class UserGroupEC : IEntityTypeConfiguration<UserGroup> , ISeed<UserGroup>
    {
        public void Configure(EntityTypeBuilder<UserGroup> builder)
        {
            builder.HasIndex(x => x.Deleted);
            builder.HasKey(x => x.Id);
        }
        public List<UserGroup> GetSeedData()
        {
            var admin = new UserGroup("Admin",false);
            admin.SetId(Guid.Parse(InitConst.AdminGuid));
            var user = new UserGroup("User",false);
            user.SetId(Guid.Parse(InitConst.UserGuid));
            return [admin,user];
        }
    }
}
