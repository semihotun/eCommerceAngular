using eCommerceBase.Domain.AggregateModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace eCommerceBase.Persistence.EntityConfigurations
{
    public class MailTemplateKeywordEC : IEntityTypeConfiguration<MailTemplateKeyword>
    {
        public void Configure(EntityTypeBuilder<MailTemplateKeyword> builder)
        {
            builder.HasKey(x => x.Id);
        }
    }
}
