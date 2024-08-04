using eCommerceBase.Domain.AggregateModels;
using eCommerceBase.Domain.SeedWork;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace eCommerceBase.Persistence.EntityConfigurations
{
    public class MailTemplateEC : IEntityTypeConfiguration<MailTemplate>, ISeed<MailTemplate>
    {
        public void Configure(EntityTypeBuilder<MailTemplate> builder)
        {
            builder.HasKey(x => x.Id);
        }

        public List<MailTemplate> GetSeedData()
        {
            return
             [
                 new("Hesabınız saldırı altında", "Sistemimizde hesabınız için sık sık \"Şifremi Unuttum\" talebi gönderildiğini fark ettik"),
                 new MailTemplate("Hoşgeldiniz", "Hoşgeldiniz").SetId(Guid.Parse("7bd1e571-9151-405e-b0dd-60f288da2fb8"))
             ];
        }
    }
}
