using eCommerceBase.Domain.SeedWork;

namespace eCommerceBase.Domain.AggregateModels
{
    public class Page : BaseEntity
    {
        public string PageTitle { get; private set; }
        public string PageContent { get; private set; }
        public string? Slug { get; private set; }
        public Page(string pageTitle, string pageContent)
        {
            PageTitle = pageTitle;
            PageContent = pageContent;
        }
        public void SetSlug(string slug)
        {
            Slug = slug;
        }
    }
}
