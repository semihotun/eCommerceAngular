using Carter;
using eCommerceBase.Application.Handlers.Sliders.Commands;
using eCommerceBase.Application.Handlers.Sliders.Queries;
using eCommerceBase.Domain.AggregateModels;
using eCommerceBase.Domain.Result;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace eCommerceBase.Api.Endpoints
{
    public class SliderEndpoints : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            var group = app.MapGroup("api/slider");

            group.MapPost("/createslider", CreateSlider)
                .Produces(StatusCodes.Status200OK, typeof(Result))
                .DisableAntiforgery()
                .AllowAnonymous();
            group.MapPost("/updateslider", UpdateSlider)
                .Produces(StatusCodes.Status200OK, typeof(Result))
                .AllowAnonymous();
            group.MapPost("/deleteslider", DeleteSlider)
                .Produces(StatusCodes.Status200OK, typeof(Result))
                .AllowAnonymous();
            group.MapGet("/getsliderbyid", GetSliderById)
               .Produces(StatusCodes.Status200OK, typeof(Result<Slider>))
               .AllowAnonymous();
            group.MapGet("/getallslider", GetAllSlider)
            .Produces(StatusCodes.Status200OK, typeof(Result<Slider>))
            .AllowAnonymous();
        }
        public static async Task<IResult> CreateSlider([FromForm] CreateSliderCommand data, ISender sender)
        {
            var result = await sender.Send(data);
            if (result.Success)
            {
                return Results.Ok(result);
            }
            return Results.BadRequest(result);
        }
        public static async Task<IResult> UpdateSlider([FromForm] UpdateSliderCommand data, ISender sender)
        {
            var result = await sender.Send(data);
            if (result.Success)
            {
                return Results.Ok(result);
            }
            return Results.BadRequest(result);
        }
        public static async Task<IResult> DeleteSlider([FromBody] DeleteSliderCommand data, ISender sender)
        {
            var result = await sender.Send(data);
            if (result.Success)
            {
                return Results.Ok(result);
            }
            return Results.BadRequest(result);
        }
        public static async Task<IResult> GetSliderById([FromQuery] Guid id, ISender sender)
        {
            var result = await sender.Send(new GetSliderByIdQuery(id));
            if (result.Success)
            {
                return Results.Ok(result);
            }
            return Results.BadRequest(result);
        }
        public static async Task<IResult> GetAllSlider(ISender sender)
        {
            var result = await sender.Send(new GetAllSlider());
            if (result.Success)
            {
                return Results.Ok(result);
            }
            return Results.BadRequest(result);
        }
    }
}
