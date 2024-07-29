using Riok.Mapperly.Abstractions;
using eCommerceBase.Application.Handlers.SpecificationAttributes.Commands;
using eCommerceBase.Domain.AggregateModels;
using eCommerceBase.Application.Handlers.Mapper;

namespace eCommerceBase.Application.Handlers.Mapper
{
    [Mapper()]
    public static partial class SpecificationAttributeMapper
    {
        public static partial SpecificationAttribute CreateSpecificationAttributeCommandToSpecificationAttribute(CreateSpecificationAttributeCommand specificationAttribute);
        public static partial SpecificationAttribute UpdateSpecificationAttributeCommandToSpecificationAttribute(UpdateSpecificationAttributeCommand specificationAttribute);
    }
}