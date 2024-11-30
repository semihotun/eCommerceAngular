using eCommerceBase.Domain.AggregateModels;
using eCommerceBase.Domain.SeedWork;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace eCommerceBase.Persistence.EntityConfigurations
{
    public class ShowCaseTypeEC : IEntityTypeConfiguration<ShowCaseType>, ISeed<ShowCaseType>
    {
        public void Configure(EntityTypeBuilder<ShowCaseType> builder)
        {
            builder.HasIndex(x => x.Deleted);
            builder.HasKey(x => x.Id);
        }

        public List<ShowCaseType> GetSeedData()
        {
            var showcase1 = new ShowCaseType("Ürün Slider");
            showcase1.SetId(Guid.Parse("6f9619ff-8b86-d011-b42d-00c04fc964ff"));
            var showcase2 = new ShowCaseType("8'li Ürün");
            showcase2.SetId(Guid.Parse("a920bc9e-58d7-48ca-86e8-d71fa4e71764"));
            var showcase3 = new ShowCaseType("Yazı");
            showcase3.SetId(Guid.Parse("9c8e872b-b98e-491c-bed2-7484dfc26620"));
            return [showcase1, showcase2, showcase3];
        }
    }
}
