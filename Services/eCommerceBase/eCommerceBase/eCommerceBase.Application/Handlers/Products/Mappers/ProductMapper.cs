using Riok.Mapperly.Abstractions;
using eCommerceBase.Application.Handlers.Products.Commands;
using eCommerceBase.Domain.AggregateModels;
using eCommerceBase.Application.Handlers.Mapper;

namespace eCommerceBase.Application.Handlers.Mapper
{
    [Mapper]
    public static partial class ProductMapper
    {
        public static partial Product CreateProductCommandToProduct(CreateProductCommand product);
        public static partial Product UpdateProductCommandToProduct(UpdateProductCommand product);
    }
}