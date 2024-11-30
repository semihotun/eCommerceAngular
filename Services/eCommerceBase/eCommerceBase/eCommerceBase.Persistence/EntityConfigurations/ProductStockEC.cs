using eCommerceBase.Domain.AggregateModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace eCommerceBase.Persistence.EntityConfigurations
{
    public class ProductStockEC : IEntityTypeConfiguration<ProductStock>
    {
        public void Configure(EntityTypeBuilder<ProductStock> builder)
        {
            builder.HasKey(x => x.Id);
            builder.HasIndex(x => x.WarehouseId);
            builder.HasIndex(x => x.ProductId);
            builder.HasIndex(x => x.Deleted);
            builder.HasIndex(x => x.CurrencyId);
        }
    }
}
