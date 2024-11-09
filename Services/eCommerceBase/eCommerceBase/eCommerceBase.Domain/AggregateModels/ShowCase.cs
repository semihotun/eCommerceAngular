using eCommerceBase.Domain.SeedWork;

namespace eCommerceBase.Domain.AggregateModels
{
    public class ShowCase : BaseEntity
    {
        public int? ShowCaseOrder { get; private set; }
        public string ShowCaseTitle { get; private set; }
        public Guid? ShowCaseTypeId { get; private set; }
        public string ShowCaseText { get; private set; }

        public ShowCase(int? showCaseOrder, string showCaseTitle, Guid? showCaseTypeId, string showCaseText)
        {
            ShowCaseOrder = showCaseOrder;
            ShowCaseTitle = showCaseTitle;
            ShowCaseTypeId = showCaseTypeId;
            ShowCaseText = showCaseText;
        }

        public ShowCaseType? ShowCaseType { get;private set; }
        [SwaggerIgnore]
        public ICollection<ShowCaseProduct> ShowCaseProductList { get; private set; } = [];
    }
}
