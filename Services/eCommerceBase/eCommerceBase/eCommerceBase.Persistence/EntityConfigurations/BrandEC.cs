using eCommerceBase.Domain.AggregateModels;
using eCommerceBase.Domain.SeedWork;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace eCommerceBase.Persistence.EntityConfigurations
{
    public class BrandEC : IEntityTypeConfiguration<Brand>,ISeed<Brand>
    {
        public void Configure(EntityTypeBuilder<Brand> builder)
        {
            builder.HasKey(x => x.Id);
            builder.HasIndex(x => x.Deleted);
        }

        public List<Brand> GetSeedData()
        {
            var brand = new Brand("ExemBrand");
            brand.SetId(Guid.Parse("d3c1b385-475e-457e-8df5-def8feef2af9"));
            return [brand];
        }
    }
}
