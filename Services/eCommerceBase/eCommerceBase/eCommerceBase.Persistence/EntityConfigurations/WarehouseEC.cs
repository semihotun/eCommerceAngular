using eCommerceBase.Domain.AggregateModels;
using eCommerceBase.Domain.SeedWork;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace eCommerceBase.Persistence.EntityConfigurations
{
    public class WarehouseEC : IEntityTypeConfiguration<Warehouse>, ISeed<Warehouse>
    {
        public void Configure(EntityTypeBuilder<Warehouse> builder)
        {
            builder.HasKey(x => x.Id);
        }

        public List<Warehouse> GetSeedData()
        {
            var data = new Warehouse("Ana Depo", "İzmir");
            data.SetId(Guid.Parse("d3c1b385-475e-457e-8df5-def8feef2af9"));
            return [data];
        }
    }
}
