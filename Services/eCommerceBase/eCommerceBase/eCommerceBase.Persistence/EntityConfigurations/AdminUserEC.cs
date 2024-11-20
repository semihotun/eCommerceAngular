using eCommerceBase.Domain.AggregateModels;
using eCommerceBase.Domain.SeedWork;
using eCommerceBase.Insfrastructure.Utilities.Security.Hashing;
using Elastic.CommonSchema;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace eCommerceBase.Persistence.EntityConfigurations
{
    public class AdminUserEC : IEntityTypeConfiguration<AdminUser>,ISeed<AdminUser>
    {
        public void Configure(EntityTypeBuilder<AdminUser> builder)
        {
            builder.HasKey(x => x.Id);
        }

        public List<AdminUser> GetSeedData()
        {
            HashingHelper.CreatePasswordHash("semih123", out byte[] passwordHash, out byte[] passwordSalt);
            var user = new AdminUser("Semih",
                "Ötün",
                "semihotun1@gmail.com",
                passwordSalt,
                passwordHash,
                true);
            user.SetAdminForUserGroup();
            return [user];
        }                                               
    }
}
