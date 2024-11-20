using eCommerceBase.Domain.Constant;
using eCommerceBase.Domain.SeedWork;
using Newtonsoft.Json;

namespace eCommerceBase.Domain.AggregateModels
{
    public class WebsiteInfo : IElasticEntity
    {
        public Guid Id { get; private set; }
        public string? SocialMediaText { get; private set; }
        public string? Logo { get; private set; }
        public string? WebSiteName { get; private set; }
        public List<SocialMediaInfo> SocialMediaInfos { get; private set; } = [];

        public WebsiteInfo(string? socialMediaText, string? logo, string? webSiteName, List<SocialMediaInfo>? socialMediaInfos)
        {
            Id = Guid.Parse(InitConst.WebSiteInfoId);
            SocialMediaText = socialMediaText;
            Logo = logo;
            WebSiteName = webSiteName;
            SocialMediaInfos = socialMediaInfos ?? [];
        }

        public void AddSocialMediaInfos(SocialMediaInfo? socialMediaInfo)
        {
            if (socialMediaInfo != null)
            {
                SocialMediaInfos.Add(socialMediaInfo);
            }
        }
    }

    public class SocialMediaInfo
    {
        public Guid? Id { get; set; }
        public SocialMediaInfo(Guid? id,string? platformName, string? url, string? icon)
        {
            Id = id ?? Guid.NewGuid();
            PlatformName = platformName;
            Url = url;
            Icon = icon;
        }
        public string? PlatformName { get; set; }
        public string? Url { get; set; }
        public string? Icon { get; set; }
    }
}