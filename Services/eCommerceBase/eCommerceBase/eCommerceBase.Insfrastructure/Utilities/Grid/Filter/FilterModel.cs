namespace eCommerceBase.Insfrastructure.Utilities.Grid.Filter
{
    /// <summary>
    /// dynamic grid filter models
    /// </summary>
    public class FilterModel(string propertyName, string filterType, string filterValue, bool jsonOrXml, string? andOrOperation)
    {
        public string PropertyName { get; set; } = propertyName;
        public string FilterType { get; set; } = filterType;
        public string FilterValue { get; set; } = filterValue;
        public bool JsonOrXml { get; set; } = jsonOrXml;
        public string? AndOrOperation { get; set; } = andOrOperation;
    }
}
