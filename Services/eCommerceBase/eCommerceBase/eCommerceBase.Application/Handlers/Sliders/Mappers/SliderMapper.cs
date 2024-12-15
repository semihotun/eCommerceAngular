using eCommerceBase.Application.Handlers.Sliders.Commands;
using eCommerceBase.Domain.AggregateModels;
using Riok.Mapperly.Abstractions;

namespace eCommerceBase.Application.Handlers.Mapper
{
    [Mapper]
    public static partial class SliderMapper
    {
        public static partial Slider CreateSliderCommandToSlider(CreateSliderCommand slider);
        public static partial Slider UpdateSliderCommandToSlider(UpdateSliderCommand slider);
    }
}