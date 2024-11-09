using eCommerceBase.Domain.SeedWork;

namespace eCommerceBase.Domain.AggregateModels
{
    public class ShowCaseType : BaseEntity
    {
        public string Type { get; private set; }

        public ShowCaseType(string type)
        {
            Type = type;
        }
        [SwaggerIgnore]
        public ICollection<ShowCase> ShowCaseList { get; private set; } = [];
    }
}
