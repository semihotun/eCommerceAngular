using Riok.Mapperly.Abstractions;
using eCommerceBase.Application.Handlers.ProductSpecifications.Commands;
using eCommerceBase.Domain.AggregateModels;
using eCommerceBase.Application.Handlers.Mapper;

namespace eCommerceBase.Application.Handlers.Mapper
{
    [Mapper]
    public static partial class ProductSpecificationMapper
    {
        public static partial ProductSpecification CreateProductSpecificationCommandToProductSpecification(CreateProductSpecificationCommand productSpecification);
        public static partial ProductSpecification UpdateProductSpecificationCommandToProductSpecification(UpdateProductSpecificationCommand productSpecification);
    }
}