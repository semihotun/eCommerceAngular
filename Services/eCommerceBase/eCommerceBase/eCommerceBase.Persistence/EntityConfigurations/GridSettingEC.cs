using eCommerceBase.Domain.AggregateModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace eCommerceBase.Persistence.EntityConfigurations
{
    public class GridSettingEc : IEntityTypeConfiguration<GridSetting>
    {
        public void Configure(EntityTypeBuilder<GridSetting> builder)
        {
            builder.HasKey(x => x.Id);
            builder.HasIndex(x => x.Deleted);
        }
    }
}
