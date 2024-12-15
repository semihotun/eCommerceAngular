using eCommerceBase.Application.Handlers.SpecificationAttributes.Commands;
using eCommerceBase.Domain.AggregateModels;
using Riok.Mapperly.Abstractions;

namespace eCommerceBase.Application.Handlers.Mapper
{
    [Mapper()]
    public static partial class SpecificationAttributeMapper
    {
        public static partial SpecificationAttribute CreateSpecificationAttributeCommandToSpecificationAttribute(CreateSpecificationAttributeCommand specificationAttribute);
        public static partial SpecificationAttribute UpdateSpecificationAttributeCommandToSpecificationAttribute(UpdateSpecificationAttributeCommand specificationAttribute);
    }
}