using eCommerceBase.Domain.AggregateModels;
using eCommerceBase.Domain.SeedWork;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace eCommerceBase.Persistence.EntityConfigurations
{
    public class MailTemplateKeywordEC : IEntityTypeConfiguration<MailTemplateKeyword>,ISeed<MailTemplateKeyword>
    {
        public void Configure(EntityTypeBuilder<MailTemplateKeyword> builder)
        {
            builder.HasKey(x => x.Id);
        }

        public List<MailTemplateKeyword> GetSeedData()
        {
            return [
                    new MailTemplateKeyword(Guid.Parse("7bd1e571-9151-405e-b0dd-60f288da2fb8"),"email","Email"),
                new MailTemplateKeyword(Guid.Parse("7bd1e571-9151-405e-b0dd-60f288da2fb8"), "name", "İsim"),
                new MailTemplateKeyword(Guid.Parse("7bd1e571-9151-405e-b0dd-60f288da2fb8"), "surname", "Soy İsim"),
            ];
        }
    }
}
