using eCommerceBase.Domain.AggregateModels;
using eCommerceBase.Domain.SeedWork;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace eCommerceBase.Persistence.EntityConfigurations
{
    public class SpecificationAttributeEC : IEntityTypeConfiguration<SpecificationAttribute>, ISeed<SpecificationAttribute>
    {
        public void Configure(EntityTypeBuilder<SpecificationAttribute> builder)
        {
            builder.HasKey(x => x.Id);
            builder.HasIndex(x => x.Deleted);
        }

        public List<SpecificationAttribute> GetSeedData()
        {
            var spec1Id = Guid.NewGuid();
            var spec1 = new SpecificationAttribute("Renk");
            spec1.SetId(spec1Id);
            spec1.AddSpecificationAttributeOption(new(spec1Id,"Kırmızı"));
            spec1.AddSpecificationAttributeOption(new(spec1Id, "Yeşil"));
            spec1.AddSpecificationAttributeOption(new(spec1Id, "Mavi"));
            spec1.AddSpecificationAttributeOption(new(spec1Id, "Turuncu"));
            spec1.AddSpecificationAttributeOption(new(spec1Id, "Sarı"));


            var spec2Id = Guid.NewGuid();
            var spec2 = new SpecificationAttribute("Kıyafet Boyutu");
            spec2.SetId(spec2Id);
            spec2.AddSpecificationAttributeOption(new(spec2Id, "L"));
            spec2.AddSpecificationAttributeOption(new(spec2Id, "XL"));
            spec2.AddSpecificationAttributeOption(new(spec2Id, "M"));
            spec2.AddSpecificationAttributeOption(new(spec2Id, "S"));
            spec2.AddSpecificationAttributeOption(new(spec2Id, "XXL"));


            return [spec1,spec2];
        }
    }
}
