using eCommerceBase.Domain.AggregateModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace eCommerceBase.Persistence.EntityConfigurations
{
    public class DiscountProductEC : IEntityTypeConfiguration<DiscountProduct>
    {
        public void Configure(EntityTypeBuilder<DiscountProduct> builder)
        {
            builder.HasKey(x => x.Id);
            builder.HasOne(x => x.ProductStock)
             .WithMany(p => p.DiscountProductList)
             .HasForeignKey(x => x.ProductStockId);
        }
    }
}
