using Carter;

namespace eCommerceBase.Api.Endpoints
{
    public class CityAndStateEndpoints : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            var group = app.MapGroup("api/cityandstate");
        }
    }
}
