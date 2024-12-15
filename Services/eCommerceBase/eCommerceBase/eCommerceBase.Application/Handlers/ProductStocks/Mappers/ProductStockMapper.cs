using eCommerceBase.Application.Handlers.ProductStocks.Commands;
using eCommerceBase.Domain.AggregateModels;
using Riok.Mapperly.Abstractions;

namespace eCommerceBase.Application.Handlers.Mapper
{
    [Mapper]
    public static partial class ProductStockMapper
    {
        public static partial ProductStock CreateProductStockCommandToProductStock(CreateProductStockCommand productStock);
        public static partial ProductStock UpdateProductStockCommandToProductStock(UpdateProductStockCommand productStock);
    }
}