using Carter;
using eCommerceBase.Application.Handlers.WebsiteInfoes.Commands;
using eCommerceBase.Application.Handlers.WebsiteInfoes.Queries;
using eCommerceBase.Domain.AggregateModels;
using eCommerceBase.Domain.Result;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace eCommerceBase.Api.Endpoints
{
    public class WebsiteInfoEndpoints : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            var group = app.MapGroup("api/websiteInfo");

            group.MapPost("/update", UpdateWebsiteInfo)
                .Produces(StatusCodes.Status200OK, typeof(Result));
            group.MapGet("/get", GetWebsiteInfoById)
               .Produces(StatusCodes.Status200OK, typeof(Result<WebsiteInfo>))
               .AllowAnonymous();
        }   
        public static async Task<IResult> UpdateWebsiteInfo([FromBody] UpdateWebsiteInfoCommand data, ISender sender)
        {
            var result = await sender.Send(data);
            if (result.Success)
            {
                return Results.Ok(result);
            }
            return Results.BadRequest(result);
        }      
        public static async Task<IResult> GetWebsiteInfoById(ISender sender)
        {
            var result = await sender.Send(new GetWebsiteInfoQuery());
            if (result.Success)
            {
                return Results.Ok(result);
            }
            return Results.BadRequest(result);
        }
    }
}
