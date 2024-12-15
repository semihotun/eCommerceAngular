using eCommerceBase.Application.Handlers.ProductSpecifications.Commands;
using eCommerceBase.Domain.AggregateModels;
using Riok.Mapperly.Abstractions;

namespace eCommerceBase.Application.Handlers.Mapper
{
    [Mapper]
    public static partial class ProductSpecificationMapper
    {
        public static partial ProductSpecification CreateProductSpecificationCommandToProductSpecification(CreateProductSpecificationCommand productSpecification);
        public static partial ProductSpecification UpdateProductSpecificationCommandToProductSpecification(UpdateProductSpecificationCommand productSpecification);
    }
}