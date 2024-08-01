namespace eCommerceBase.Application.Handlers.Categories.Queries.Dtos
{
    public class CategoryTreeDTO
    {
        public Guid Id { get;  set; }
        public string? CategoryName { get;  set; }
        public List<CategoryTreeDTO> SubCategories { get; set; } = [];
    }
}
