using eCommerceBase.Domain.AggregateModels;
using eCommerceBase.Domain.SeedWork;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace eCommerceBase.Persistence.EntityConfigurations
{
    public class CurrencyEC : IEntityTypeConfiguration<Currency>, ISeed<Currency>
    {
        public void Configure(EntityTypeBuilder<Currency> builder)
        {
            builder.HasKey(x => x.Id);
            builder.HasIndex(x => x.Deleted);
        }

        public List<Currency> GetSeedData()
        {
            var tlData = new Currency("₺", "TL", "Türk Lirası");
            tlData.SetId(Guid.Parse("d3c1b385-475e-457e-8df5-def8feef2af9"));
            return [
            new Currency("$","USD","Dollar"),
            tlData,
            new Currency("€ ","EUR","EURO")
            ];
        }
    }
}
