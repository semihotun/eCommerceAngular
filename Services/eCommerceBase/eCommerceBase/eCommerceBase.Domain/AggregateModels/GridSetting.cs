using eCommerceBase.Domain.SeedWork;

namespace eCommerceBase.Domain.AggregateModels
{
    public class GridSetting : BaseEntity
    {
        public string Path { get; set; }

        public string PropertyInfo { get; set; }

        public GridSetting(string path, string propertyInfo)
        {
            Path = path;
            PropertyInfo = propertyInfo;
        }
    }
}
