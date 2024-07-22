using Riok.Mapperly.Abstractions;
using eCommerceBase.Application.Handlers.Sliders.Commands;
using eCommerceBase.Domain.AggregateModels;
using eCommerceBase.Application.Handlers.Mapper;

namespace eCommerceBase.Application.Handlers.Mapper
{
    [Mapper]
    public static partial class SliderMapper
    {
        public static partial Slider CreateSliderCommandToSlider(CreateSliderCommand slider);
        public static partial void UpdateSliderCommandToSlider(UpdateSliderCommand updateSliderCommand, Slider slider);
    }
}