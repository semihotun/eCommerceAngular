using Carter;
using eCommerceBase.Application.Handlers.ShowCaseProducts.Commands;
using eCommerceBase.Application.Handlers.ShowCaseProducts.Queries;
using eCommerceBase.Application.Handlers.ShowCaseProducts.Queries.Dtos;
using eCommerceBase.Domain.AggregateModels;
using eCommerceBase.Domain.Result;
using eCommerceBase.Insfrastructure.Utilities.Grid.PagedList;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace eCommerceBase.Api.Endpoints
{
    public class ShowCaseProductShowcaseProductEndpointsEndpoints : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            var group = app.MapGroup("api/showCaseProduct");

            group.MapPost("/create", CreateShowCaseProduct)
                .Produces(StatusCodes.Status200OK, typeof(Result));
            group.MapPost("/update", UpdateShowCaseProduct)
                .Produces(StatusCodes.Status200OK, typeof(Result))
                .AllowAnonymous();
            group.MapPost("/delete", DeleteShowCaseProduct)
                .Produces(StatusCodes.Status200OK, typeof(Result))
                .AllowAnonymous();
            group.MapGet("/getbyid", GetShowCaseProductById)
               .Produces(StatusCodes.Status200OK, typeof(Result<ShowCaseProduct>))
               .AllowAnonymous();
            group.MapGet("/getall", GetAllShowCaseProduct)
                .Produces(StatusCodes.Status200OK, typeof(Result<ShowCaseProduct>))
                .AllowAnonymous();
            group.MapPost("/getgrid", GetShowCaseProductGrid)
                .Produces(StatusCodes.Status200OK, typeof(Result<IPagedList<ShowCaseProductGrid>>))
                .AllowAnonymous();
        }
        public static async Task<IResult> CreateShowCaseProduct([FromBody] CreateShowCaseProductCommand data, ISender sender)
        {
            var result = await sender.Send(data);
            if (result.Success)
            {
                return Results.Ok(result);
            }
            return Results.BadRequest(result);
        }
        public static async Task<IResult> UpdateShowCaseProduct([FromBody] UpdateShowCaseProductCommand data, ISender sender)
        {
            var result = await sender.Send(data);
            if (result.Success)
            {
                return Results.Ok(result);
            }
            return Results.BadRequest(result);
        }
        public static async Task<IResult> DeleteShowCaseProduct([FromBody] DeleteShowCaseProductCommand data, ISender sender)
        {
            var result = await sender.Send(data);
            if (result.Success)
            {
                return Results.Ok(result);
            }
            return Results.BadRequest(result);
        }
        public static async Task<IResult> GetShowCaseProductById([FromQuery] Guid id, ISender sender)
        {
            var result = await sender.Send(new GetShowCaseProductByIdQuery(id));
            if (result.Success)
            {
                return Results.Ok(result);
            }
            return Results.BadRequest(result);
        }
        public static async Task<IResult> GetAllShowCaseProduct(ISender sender)
        {
            var result = await sender.Send(new GetAllShowCaseProduct());
            if (result.Success)
            {
                return Results.Ok(result);
            }
            return Results.BadRequest(result);
        }
        public static async Task<IResult> GetShowCaseProductGrid([FromBody] GetShowCaseProductGridQuery data, ISender sender)
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
