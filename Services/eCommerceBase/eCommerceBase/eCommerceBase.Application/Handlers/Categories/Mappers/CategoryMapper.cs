using eCommerceBase.Application.Handlers.Categories.Commands;
using eCommerceBase.Domain.AggregateModels;
using Riok.Mapperly.Abstractions;

namespace eCommerceBase.Application.Handlers.Mapper
{
    [Mapper]
    public static partial class CategoryMapper
    {
        public static partial Category CreateCategoryCommandToCategory(CreateCategoryCommand category);
        public static partial Category UpdateCategoryCommandToCategory(UpdateCategoryCommand category);
    }
}