using eCommerceBase.Domain.AggregateModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace eCommerceBase.Persistence.EntityConfigurations
{
    public class ProductCommentEC : IEntityTypeConfiguration<ProductComment>
    {
        public void Configure(EntityTypeBuilder<ProductComment> builder)
        {
            builder.HasKey(x => x.Id);
            builder.HasIndex(x => x.ProductId);
            builder.HasIndex(x => x.CustomerUserId);
            builder.HasIndex(x => x.IsApprove);
        }
    }
}
