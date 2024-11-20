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
        }

        public List<ShowCase> GetSeedData()
        {
            var productId = Guid.Parse("d3c1b385-475e-457e-8df5-def8feef2af9");
            //Showcase 1
            var showCaseProductSliderId = Guid.Parse("251da5aa-819c-42f7-8e13-078b8e864951");
            var showCaseProductSlider = new ShowCase(1, "Ürün Slider", ShowcaseConst.ProductSlider, null);
            showCaseProductSlider.SetId(showCaseProductSliderId);
            showCaseProductSlider.AddShowCaseProductList(new ShowCaseProduct(showCaseProductSliderId, productId));
            //Showcase 2
            var showCase8X8ProductId = Guid.Parse("05637e5b-5dda-4955-b5fb-2bd41f67aef7");
            var showCase8X8Urun = new ShowCase(2, "8X8 Ürün", ShowcaseConst.Product8x8, null);
            showCase8X8Urun.SetId(showCase8X8ProductId);
            showCase8X8Urun.AddShowCaseProductList(new ShowCaseProduct(showCase8X8ProductId, productId));

            return [
               showCase8X8Urun,showCaseProductSlider,
                new ShowCase(3,"Yazı & Banner", ShowcaseConst.Text,"Ürünlerde 50% İndirim"),
            ];
        }
    }
}
