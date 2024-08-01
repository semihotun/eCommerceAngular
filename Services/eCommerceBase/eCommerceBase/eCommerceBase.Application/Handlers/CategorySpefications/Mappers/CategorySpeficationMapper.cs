using Riok.Mapperly.Abstractions;
using eCommerceBase.Application.Handlers.CategorySpefications.Commands;
using eCommerceBase.Domain.AggregateModels;
using eCommerceBase.Application.Handlers.Mapper;

namespace eCommerceBase.Application.Handlers.Mapper
{
    [Mapper]
    public static partial class CategorySpeficationMapper
    {
        public static partial CategorySpecification CreateCategorySpeficationCommandToCategorySpefication(CreateCategorySpeficationCommand categorySpefication);
        public static partial CategorySpecification UpdateCategorySpeficationCommandToCategorySpefication(UpdateCategorySpeficationCommand categorySpefication);
    }
}