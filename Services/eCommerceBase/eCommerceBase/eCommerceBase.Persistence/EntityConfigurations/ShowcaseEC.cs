using Bogus;
using eCommerceBase.Domain.AggregateModels;
using eCommerceBase.Domain.Constant;
using eCommerceBase.Domain.SeedWork;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace eCommerceBase.Persistence.EntityConfigurations
{
    public class ShowcaseEC : IEntityTypeConfiguration<ShowCase>, ISeed<ShowCase>
    {
        public void Configure(EntityTypeBuilder<ShowCase> builder)
        {
            builder.HasKey(x => x.Id);
            builder.HasIndex(x => x.ShowCaseTypeId);
            builder.HasIndex(x => x.Deleted);
        }

        public List<ShowCase> GetSeedData()
        {
            //Showcase 1
            var showCaseProductSliderId = Guid.Parse("251da5aa-819c-42f7-8e13-078b8e864951");
            var showCaseProductSlider = new ShowCase(1, "Ürün Slider", ShowcaseConst.ProductSlider, null);
            showCaseProductSlider.SetId(showCaseProductSliderId);

            //Showcase 2
            var showCase8X8ProductId = Guid.Parse("05637e5b-5dda-4955-b5fb-2bd41f67aef7");
            var showCase8X8Urun = new ShowCase(2, "8X8 Ürün", ShowcaseConst.Product8x8, null);
            showCase8X8Urun.SetId(showCase8X8ProductId);

            /// Ürünler İnit
            var wareHouseId = Guid.Parse("d3c1b385-475e-457e-8df5-def8feef2af9");
            var currencyId = Guid.Parse("d3c1b385-475e-457e-8df5-def8feef2af9");
            var brandId = Guid.Parse("d3c1b385-475e-457e-8df5-def8feef2af9");
            var categoryId = Guid.Parse("d3c1b385-475e-457e-8df5-def8feef2af9");

            var productFaker = new Faker<Product>()
                .CustomInstantiator(f => new Product(
                     f.Commerce.ProductName(), brandId, categoryId, f.Lorem.Sentence(), f.Commerce.Ean13(), f.Commerce.Ean8()
                ));


            for (int i = 0; i < 10; i++)
            {
                var productId = Guid.NewGuid();
                var product = productFaker.Generate();
                product.SetId(productId);
                product.SetSlug($"slug-{i + 1}");
                var productPhoto = new ProductPhoto(productId, new Faker().Image.PicsumUrl(400, 300));
                product.AddProductPhotoList(productPhoto);
                var productStock = new ProductStock(10, 10, wareHouseId, productId, new Random().Next(10, 1000), currencyId);
                var discountProduct = new DiscountProduct(
                    DiscountTypeConst.ProductPercentDiscount,
                    productStock.Id,
                    new Random().Next(1, 20)
                );
                productStock.AddDiscountProductList(discountProduct);
                product.AddProductStockList(productStock);

                var showcaseProduct1 = new ShowCaseProduct(showCaseProductSliderId, productId);
                showcaseProduct1.SetProduct(product);
                showCaseProductSlider.AddShowCaseProductList(showcaseProduct1);

                var showcaseProduct2 = new ShowCaseProduct(showCase8X8ProductId, productId);
                showcaseProduct1.SetProduct(product);

                showCase8X8Urun.AddShowCaseProductList(showcaseProduct2);
            }

            return [
               showCase8X8Urun,showCaseProductSlider,
                new ShowCase(3,"Yazı & Banner", ShowcaseConst.Text,"Ürünlerde 50% İndirim"),
            ];
        }
    }
}
