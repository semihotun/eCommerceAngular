using eCommerceBase.Domain.AggregateModels;
using eCommerceBase.Domain.SeedWork;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.IdentityModel.Logging;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace eCommerceBase.Persistence.EntityConfigurations
{
    public class ProductEC : IEntityTypeConfiguration<Product>, ISeed<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.HasKey(x => x.Id);
        }

        public List<Product> GetSeedData()
        {
            var product = new Product("ExemProducy", Guid.Parse("d3c1b385-475e-457e-8df5-def8feef2af9"),
                Guid.Parse("d3c1b385-475e-457e-8df5-def8feef2af9"), "Exem Content", "Exem Gtin", "Exem Sku");
            product.SetId(Guid.Parse("d3c1b385-475e-457e-8df5-def8feef2af9"));
            product.SetSlug("exem-product");
            return [product];
        }
    }
}
