using Carter;
using eCommerceBase.Application.Handlers.Cities.Queries;
using eCommerceBase.Application.Handlers.States.Queries;
using eCommerceBase.Domain.AggregateModels;
using eCommerceBase.Domain.Result;
using eCommerceBase.Insfrastructure.Utilities.Grid.PagedList;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace eCommerceBase.Api.Endpoints
{
    public class CityAndStateEndpoints : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            var group = app.MapGroup("api/cityanddistrict");
            group.MapPost("/getallcity", GetAllCity)
                .Produces(StatusCodes.Status200OK, typeof(Result<PagedList<City>>))
                .AllowAnonymous();
            group.MapPost("/getalldistrictbycityid", GetAllDistrictByCityId)
              .Produces(StatusCodes.Status200OK, typeof(Result<PagedList<District>>))
              .AllowAnonymous();
        }
        public static async Task<IResult> GetAllCity([FromBody] GetAllCityQuery data, ISender sender)
        {
            var result = await sender.Send(data);
            if (result.Success)
            {
                return Results.Ok(result);
            }
            return Results.BadRequest(result);
        }
        public static async Task<IResult> GetAllDistrictByCityId([FromBody] GetAllDistrictByCityIdQuery data, ISender sender)
        {
            var result = await sender.Send(data);
            if (result.Success)
            {
                return Results.Ok(result);
            }
            return Results.BadRequest(result);
        }
    }
}
