using eCommerceBase.Domain.AggregateModels;
using eCommerceBase.Domain.Constant;
using eCommerceBase.Domain.SeedWork;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace eCommerceBase.Persistence.EntityConfigurations
{
    public class DiscountTypeEC : IEntityTypeConfiguration<DiscountType>, ISeed<DiscountType>
    {
        public void Configure(EntityTypeBuilder<DiscountType> builder)
        {
           builder.HasKey(x => x.Id);
        }

        public List<DiscountType> GetSeedData()
        {
            var discount1 = new Discount("Ürün % İndirimi", DiscountTypeConst.ProductPercentDiscount);
            discount1.SetId(DiscountTypeConst.ProductPercentDiscount);
            var disCountType1 = new DiscountType("Ürün % İndirimi");
            disCountType1.SetId(DiscountTypeConst.ProductPercentDiscount);
            disCountType1.AddDiscountList(discount1);

            var disCountType2 = new DiscountType("Ürün - İndirimi");
            disCountType2.SetId(DiscountTypeConst.ProductCurrencyDiscount);
            var discount2 = new Discount("Ürün Birim İndirimi", DiscountTypeConst.ProductCurrencyDiscount);
            discount2.SetId(DiscountTypeConst.ProductCurrencyDiscount);
            disCountType2.AddDiscountList(discount2);

            var disCountType3 = new DiscountType("Kategori % İndirimi");
            disCountType3.SetId(DiscountTypeConst.CategoryPercentDiscount);

            var disCountType4 = new DiscountType("Kategori - İndirimi");
            disCountType4.SetId(DiscountTypeConst.CategoryCurrencyDiscount);

            return [disCountType1,disCountType2,disCountType3, disCountType4];
        }
    }
}
