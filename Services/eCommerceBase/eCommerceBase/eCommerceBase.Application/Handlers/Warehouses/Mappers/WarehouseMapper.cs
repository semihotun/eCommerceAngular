using eCommerceBase.Application.Handlers.Warehouses.Commands;
using eCommerceBase.Domain.AggregateModels;
using Riok.Mapperly.Abstractions;

namespace eCommerceBase.Application.Handlers.Mapper
{
    [Mapper]
    public static partial class WarehouseMapper
    {
        public static partial Warehouse CreateWarehouseCommandToWarehouse(CreateWarehouseCommand warehouse);
        public static partial Warehouse UpdateWarehouseCommandToWarehouse(UpdateWarehouseCommand warehouse);
    }
}