using Riok.Mapperly.Abstractions;
using eCommerceBase.Application.Handlers.GridSettings.Commands;
using eCommerceBase.Domain.AggregateModels;
using eCommerceBase.Application.Handlers.Mapper;

namespace eCommerceBase.Application.Handlers.Mapper
{
    [Mapper]
    public static partial class GridSettingMapper
    {
        public static partial GridSetting CreateGridSettingCommandToGridSetting(CreateGridSettingCommand gridSetting);
        public static partial GridSetting UpdateGridSettingCommandToNewGridSetting(UpdateGridSettingCommand gridSetting);
        public static partial void UpdateGridSettingCommandToGridSetting(UpdateGridSettingCommand updateGridSettingCommand, GridSetting gridSetting);
    }
}