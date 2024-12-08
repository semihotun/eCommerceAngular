using Carter;
using eCommerceBase.Application.Handlers.Categories.Commands;
using eCommerceBase.Application.Handlers.Categories.Queries;
using eCommerceBase.Application.Handlers.Categories.Queries.Dtos;
using eCommerceBase.Domain.AggregateModels;
using eCommerceBase.Domain.Result;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace eCommerceBase.Api.Endpoints
{
    public class CategoryEndpoints : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            var group = app.MapGroup("api/category");

            group.MapPost("/create", CreateCategory)
                .Produces(StatusCodes.Status200OK, typeof(Result))
                .DisableAntiforgery()
                .AllowAnonymous();
            group.MapPost("/update", UpdateCategory)
                .Produces(StatusCodes.Status200OK, typeof(Result))
                .AllowAnonymous();
            group.MapPost("/delete", DeleteCategory)
                .Produces(StatusCodes.Status200OK, typeof(Result))
                .AllowAnonymous();
            group.MapGet("/getbyid", GetCategoryById)
               .Produces(StatusCodes.Status200OK, typeof(Result<Category>))
               .AllowAnonymous();
            group.MapGet("/getall", GetAllCategory)
                .Produces(StatusCodes.Status200OK, typeof(Result<List<CategoryTreeDTO>>))
                .AllowAnonymous();
        }
        public static async Task<IResult> CreateCategory([FromBody] CreateCategoryCommand data, ISender sender)
        {
            var result = await sender.Send(data);
            if (result.Success)
            {
                return Results.Ok(result);
            }
            return Results.BadRequest(result);
        }
        public static async Task<IResult> UpdateCategory([FromBody] UpdateCategoryCommand data, ISender sender)
        {
            var result = await sender.Send(data);
            if (result.Success)
            {
                return Results.Ok(result);
            }
            return Results.BadRequest(result);
        }
        public static async Task<IResult> DeleteCategory([FromBody] DeleteCategoryCommand data, ISender sender)
        {
            var result = await sender.Send(data);
            if (result.Success)
            {
                return Results.Ok(result);
            }
            return Results.BadRequest(result);
        }
        public static async Task<IResult> GetCategoryById([FromQuery] Guid id, ISender sender)
        {
            var result = await sender.Send(new GetCategoryByIdQuery(id));
            if (result.Success)
            {
                return Results.Ok(result);
            }
            return Results.BadRequest(result);
        }
        public static async Task<IResult> GetAllCategory(ISender sender)
        {
            var result = await sender.Send(new GetAllCategory());
            if (result.Success)
            {
                return Results.Ok(result);
            }
            return Results.BadRequest(result);
        }
    }
}
