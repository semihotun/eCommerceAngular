using Carter;
using eCommerceBase.Application.Handlers.Brands.Queries;
using eCommerceBase.Application.Handlers.Brands.Queries.Dtos;
using eCommerceBase.Application.Handlers.ProductPhotoes.Commands;
using eCommerceBase.Application.Handlers.ProductPhotoes.Queries;
using eCommerceBase.Application.Handlers.ProductPhotoes.Queries.Dtos;
using eCommerceBase.Domain.AggregateModels;
using eCommerceBase.Domain.Result;
using eCommerceBase.Insfrastructure.Utilities.Grid.PagedList;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace eCommerceBase.Api.Endpoints
{
    public class ProductPhotoEndpoints : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            var group = app.MapGroup("api/productPhoto");

            group.MapPost("/create", CreateProductPhoto)
                .Produces(StatusCodes.Status200OK, typeof(Result))
                .AllowAnonymous();
            group.MapPost("/update", UpdateProductPhoto)
                .Produces(StatusCodes.Status200OK, typeof(Result))
                .AllowAnonymous();
            group.MapPost("/delete", DeleteProductPhoto)
                .Produces(StatusCodes.Status200OK, typeof(Result))
                .AllowAnonymous();
            group.MapGet("/getbyid", GetProductPhotoById)
               .Produces(StatusCodes.Status200OK, typeof(Result<ProductPhoto>))
               .AllowAnonymous();
            group.MapGet("/getall", GetAllProductPhoto)
                .Produces(StatusCodes.Status200OK, typeof(Result<ProductPhoto>))
                .AllowAnonymous();
            group.MapPost("/getgrid", GetProductPhotoGridById)
                .Produces(StatusCodes.Status200OK, typeof(Result<IPagedList<ProductPhotoGrid>>))
                .AllowAnonymous();
        }
        public static async Task<IResult> GetProductPhotoGridById([FromBody] GetProductPhotoGridByProductIdQuery data, ISender sender)
        {
            var result = await sender.Send(data);
            if (result.Success)
            {
                return Results.Ok(result);
            }
            return Results.BadRequest(result);
        }
        public static async Task<IResult> CreateProductPhoto([FromBody] CreateProductPhotoCommand data, ISender sender)
        {
            var result = await sender.Send(data);
            if (result.Success)
            {
                return Results.Ok(result);
            }
            return Results.BadRequest(result);
        }
        public static async Task<IResult> UpdateProductPhoto([FromBody] UpdateProductPhotoCommand data, ISender sender)
        {
            var result = await sender.Send(data);
            if (result.Success)
            {
                return Results.Ok(result);
            }
            return Results.BadRequest(result);
        }
        public static async Task<IResult> DeleteProductPhoto([FromBody] DeleteProductPhotoCommand data, ISender sender)
        {
            var result = await sender.Send(data);
            if (result.Success)
            {
                return Results.Ok(result);
            }
            return Results.BadRequest(result);
        }
        public static async Task<IResult> GetProductPhotoById([FromQuery] Guid id, ISender sender)
        {
            var result = await sender.Send(new GetProductPhotoByIdQuery(id));
            if (result.Success)
            {
                return Results.Ok(result);
            }
            return Results.BadRequest(result);
        }
        public static async Task<IResult> GetAllProductPhoto(ISender sender)
        {
            var result = await sender.Send(new GetAllProductPhoto());
            if (result.Success)
            {
                return Results.Ok(result);
            }
            return Results.BadRequest(result);
        }
    }
}
