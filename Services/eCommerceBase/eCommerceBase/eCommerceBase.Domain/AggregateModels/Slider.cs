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
        public string ImageUrl { get; private set; }
        public Slider(string sliderHeading, string sliderText, string sliderLink,string? imageUrl = null)
        {
            SliderHeading = sliderHeading;
            SliderText = sliderText;
            SliderLink = sliderLink;
            ImageUrl = imageUrl!;
        }
        public void SetSliderImage(string imageUrl)
        {
            ImageUrl = imageUrl;
        }
    }
}
