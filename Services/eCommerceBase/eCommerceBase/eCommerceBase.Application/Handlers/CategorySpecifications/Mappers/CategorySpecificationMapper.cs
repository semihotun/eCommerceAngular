using Riok.Mapperly.Abstractions;
using eCommerceBase.Application.Handlers.CategorySpecifications.Commands;
using eCommerceBase.Domain.AggregateModels;
using eCommerceBase.Application.Handlers.Mapper;

namespace eCommerceBase.Application.Handlers.Mapper
{
    [Mapper]
    public static partial class CategorySpecificationMapper
    {
        public static partial CategorySpecification CreateCategorySpecificationCommandToCategorySpecification(CreateCategorySpecificationCommand categorySpecification);
        public static partial CategorySpecification UpdateCategorySpecificationCommandToCategorySpecification(UpdateCategorySpecificationCommand categorySpecification);
    }
}