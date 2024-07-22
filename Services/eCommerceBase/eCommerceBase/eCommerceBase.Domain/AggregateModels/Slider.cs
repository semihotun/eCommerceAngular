using eCommerceBase.Domain.SeedWork;
using MediatR;
using Microsoft.AspNetCore.Http;
namespace eCommerceBase.Domain.AggregateModels
{
    public class Slider:BaseEntity
    {
        public string SliderHeading { get; private set; }
        public string SliderText { get; private set; }
        public string SliderLink { get; private set; }
        public string SliderImage { get; private set; }
        public Slider(string sliderHeading, string sliderText, string sliderLink,string? sliderImage = null)
        {
            SliderHeading = sliderHeading;
            SliderText = sliderText;
            SliderLink = sliderLink;
            SliderImage = sliderImage!;
        }
        public void SetSliderImage(string sliderImage)
        {
            SliderImage = sliderImage;
        }
    }
}
