using Riok.Mapperly.Abstractions;
using eCommerceBase.Application.Handlers.ProductShipmentInfoes.Commands;
using eCommerceBase.Domain.AggregateModels;
using eCommerceBase.Application.Handlers.Mapper;

namespace eCommerceBase.Application.Handlers.Mapper
{
    [Mapper]
    public static partial class ProductShipmentInfoMapper
    {
        public static partial ProductShipmentInfo CreateProductShipmentInfoCommandToProductShipmentInfo(CreateProductShipmentInfoCommand productShipmentInfo);
        public static partial ProductShipmentInfo UpdateProductShipmentInfoCommandToProductShipmentInfo(UpdateProductShipmentInfoCommand productShipmentInfo);
    }
}