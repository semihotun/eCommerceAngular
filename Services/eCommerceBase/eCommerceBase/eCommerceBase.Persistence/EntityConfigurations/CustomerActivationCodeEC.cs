using eCommerceBase.Domain.AggregateModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace eCommerceBase.Persistence.EntityConfigurations
{
    public class CustomerActivationCodeEC : IEntityTypeConfiguration<CustomerActivationCode>
    {
        public void Configure(EntityTypeBuilder<CustomerActivationCode> builder)
        {
           builder.HasKey(x => x.Id);
        }
    }
}
