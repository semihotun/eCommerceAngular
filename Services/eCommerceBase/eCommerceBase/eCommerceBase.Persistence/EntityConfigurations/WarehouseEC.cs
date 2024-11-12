using eCommerceBase.Domain.AggregateModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace eCommerceBase.Persistence.EntityConfigurations
{
    public class WarehouseEC : IEntityTypeConfiguration<Warehouse>
    {
        public void Configure(EntityTypeBuilder<Warehouse> builder)
        {
            builder.HasKey(x => x.Id);
        }
    }
}
