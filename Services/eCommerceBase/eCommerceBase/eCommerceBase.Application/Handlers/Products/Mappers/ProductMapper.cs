using eCommerceBase.Application.Handlers.Products.Commands;
using eCommerceBase.Domain.AggregateModels;
using Riok.Mapperly.Abstractions;

namespace eCommerceBase.Application.Handlers.Mapper
{
    [Mapper]
    public static partial class ProductMapper
    {
        public static partial Product CreateProductCommandToProduct(CreateProductCommand product);
        public static partial Product UpdateProductCommandToProduct(UpdateProductCommand product);
    }
}