using eCommerceBase.Domain.AggregateModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eCommerceBase.Persistence.EntityConfigurations
{
    public class ProduchPhotoEC : IEntityTypeConfiguration<ProductPhoto>
    {
        public void Configure(EntityTypeBuilder<ProductPhoto> builder)
        {
            builder.HasIndex(x => x.ProductId);
            builder.HasKey(x => x.Id);
            builder.HasIndex(x => x.Deleted);
        }
    }
}
