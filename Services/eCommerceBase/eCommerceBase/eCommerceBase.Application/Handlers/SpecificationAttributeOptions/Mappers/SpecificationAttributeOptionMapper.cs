using Riok.Mapperly.Abstractions;
using eCommerceBase.Application.Handlers.SpecificationAttributeOptions.Commands;
using eCommerceBase.Domain.AggregateModels;
using eCommerceBase.Application.Handlers.Mapper;

namespace eCommerceBase.Application.Handlers.Mapper
{
    [Mapper]
    public static partial class SpecificationAttributeOptionMapper
    {
        public static partial SpecificationAttributeOption CreateSpecificationAttributeOptionCommandToSpecificationAttributeOption(CreateSpecificationAttributeOptionCommand specificationAttributeOption);
        public static partial SpecificationAttributeOption UpdateSpecificationAttributeOptionCommandToSpecificationAttributeOption(UpdateSpecificationAttributeOptionCommand specificationAttributeOption);
    }
}