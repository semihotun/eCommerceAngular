using eCommerceBase.Domain.AggregateModels;
using eCommerceBase.Domain.Constant;
using eCommerceBase.Domain.SeedWork;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace eCommerceBase.Persistence.EntityConfigurations
{
    public class MailTemplateEC : IEntityTypeConfiguration<MailTemplate>, ISeed<MailTemplate>
    {
        public void Configure(EntityTypeBuilder<MailTemplate> builder)
        {
            builder.HasIndex(x => x.Deleted);
            builder.HasKey(x => x.Id);
        }

        public List<MailTemplate> GetSeedData()
        {
            var activationMailId = Guid.Parse(InitConst.ActivationMailId);
            var activationMail = new MailTemplate("Hoşgeldiniz", "Web sitesi için aktivasyon linki #{{email}}");
            activationMail.SetId(activationMailId);
            activationMail.AddMailTemplateKeywordList(new MailTemplateKeyword(activationMailId, "email", "Email"));
            activationMail.AddMailTemplateKeywordList(new MailTemplateKeyword(activationMailId, "name", "İsim"));
            activationMail.AddMailTemplateKeywordList(new MailTemplateKeyword(activationMailId, "surname", "Soy İsim"));
            activationMail.AddMailTemplateKeywordList(new MailTemplateKeyword(activationMailId, "activationCode", "Aktivasyon Kodu"));
            return
             [
                 new("Hesabınız saldırı altında", "Sistemimizde hesabınız için sık sık \"Şifremi Unuttum\" talebi gönderildiğini fark ettik"),
                 activationMail,
             ];
        }
    }
}
