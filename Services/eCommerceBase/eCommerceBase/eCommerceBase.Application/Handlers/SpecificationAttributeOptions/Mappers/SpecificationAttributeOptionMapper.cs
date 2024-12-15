using eCommerceBase.Application.Handlers.SpecificationAttributeOptions.Commands;
using eCommerceBase.Domain.AggregateModels;
using Riok.Mapperly.Abstractions;

namespace eCommerceBase.Application.Handlers.Mapper
{
    [Mapper]
    public static partial class SpecificationAttributeOptionMapper
    {
        public static partial SpecificationAttributeOption CreateSpecificationAttributeOptionCommandToSpecificationAttributeOption(CreateSpecificationAttributeOptionCommand specificationAttributeOption);
        public static partial SpecificationAttributeOption UpdateSpecificationAttributeOptionCommandToSpecificationAttributeOption(UpdateSpecificationAttributeOptionCommand specificationAttributeOption);
    }
}