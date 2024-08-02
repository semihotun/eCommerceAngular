using eCommerceBase.Domain.AggregateModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace eCommerceBase.Persistence.EntityConfigurations
{
    public class MailTemplateEC : IEntityTypeConfiguration<MailTemplate>
    {
        public void Configure(EntityTypeBuilder<MailTemplate> builder)
        {
            builder.HasKey(x => x.Id);
        }
    }
}
