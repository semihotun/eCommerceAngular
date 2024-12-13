using Bogus;
using eCommerceBase.Domain.AggregateModels;
using eCommerceBase.Domain.SeedWork;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace eCommerceBase.Persistence.EntityConfigurations
{
    public class SliderEC : IEntityTypeConfiguration<Slider>, ISeed<Slider>
    {
        public void Configure(EntityTypeBuilder<Slider> builder)
        {
            builder.HasKey(x => x.Id);
            builder.HasIndex(x => x.Deleted);
        }

        public List<Slider> GetSeedData()
        {
            var sliderFaker = new Faker<Slider>()
                .CustomInstantiator(f => new Slider("Slider Başlığı","Slider Yazısı", "", new Faker().Image.PicsumUrl(400, 300)
                ));
            var sliderList = sliderFaker.Generate(4);
            return sliderList;
        }
    }
}
