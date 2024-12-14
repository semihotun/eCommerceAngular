using eCommerceBase.Domain.AggregateModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace eCommerceBase.Persistence.EntityConfigurations
{
    public class CustomerUserAddressEC : IEntityTypeConfiguration<CustomerUserAddress>
    {
        public void Configure(EntityTypeBuilder<CustomerUserAddress> builder)
        {
            builder.HasKey(x => x.Id);
            builder.HasOne(x => x.District)
             .WithMany(d => d.CustomerUserAdressList)
             .HasForeignKey(x => x.DistrictId)
             .OnDelete(DeleteBehavior.NoAction);
        }
    }
}
