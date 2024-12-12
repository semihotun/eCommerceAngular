using eCommerceBase.Domain.AggregateModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace eCommerceBase.Persistence.EntityConfigurations
{
    public class ProductFavoriteEC : IEntityTypeConfiguration<ProductFavorite>
    {
        public void Configure(EntityTypeBuilder<ProductFavorite> builder)
        {
            builder.HasKey(x => x.Id);
            builder.HasIndex(x => x.CustomerUserId);
            builder.HasIndex(x => x.ProductId);
        }
    }
}
