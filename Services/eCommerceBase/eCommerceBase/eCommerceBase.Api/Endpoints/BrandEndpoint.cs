using Carter;
using eCommerceBase.Application.Handlers.Brands.Commands;
using eCommerceBase.Application.Handlers.Brands.Queries;
using eCommerceBase.Application.Handlers.Brands.Queries.Dtos;
using eCommerceBase.Domain.AggregateModels;
using eCommerceBase.Domain.Result;
using eCommerceBase.Insfrastructure.Utilities.Grid.PagedList;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace eCommerceBase.Api.Endpoints
{
    public class BrandEndpoints : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            var group = app.MapGroup("api/brand");

            group.MapPost("/create", CreateBrand)
                .Produces(StatusCodes.Status200OK, typeof(Result));
            group.MapPost("/update", UpdateBrand)
                .Produces(StatusCodes.Status200OK, typeof(Result))
                .AllowAnonymous();
            group.MapPost("/delete", DeleteBrand)
                .Produces(StatusCodes.Status200OK, typeof(Result))
                .AllowAnonymous();
            group.MapGet("/getbyid", GetBrandById)
               .Produces(StatusCodes.Status200OK, typeof(Result<Brand>))
               .AllowAnonymous();
            group.MapGet("/getall", GetAllBrand)
                .Produces(StatusCodes.Status200OK, typeof(Result<Brand>))
                .AllowAnonymous();
            group.MapPost("/getgrid", GetBrandGrid)
                .Produces(StatusCodes.Status200OK, typeof(Result<IPagedList<BrandGridDTO>>))
                .AllowAnonymous();
        }
        public static async Task<IResult> CreateBrand([FromBody] CreateBrandCommand data, ISender sender)
        {
            var result = await sender.Send(data);
            if (result.Success)
            {
                return Results.Ok(result);
            }
            return Results.BadRequest(result);
        }
        public static async Task<IResult> UpdateBrand([FromBody] UpdateBrandCommand data, ISender sender)
        {
            var result = await sender.Send(data);
            if (result.Success)
            {
                return Results.Ok(result);
            }
            return Results.BadRequest(result);
        }
        public static async Task<IResult> DeleteBrand([FromBody] DeleteBrandCommand data, ISender sender)
        {
            var result = await sender.Send(data);
            if (result.Success)
            {
                return Results.Ok(result);
            }
            return Results.BadRequest(result);
        }
        public static async Task<IResult> GetBrandById([FromQuery] Guid id, ISender sender)
        {
            var result = await sender.Send(new GetBrandByIdQuery(id));
            if (result.Success)
            {
                return Results.Ok(result);
            }
            return Results.BadRequest(result);
        }
        public static async Task<IResult> GetAllBrand(ISender sender)
        {
            var result = await sender.Send(new GetAllBrand());
            if (result.Success)
            {
                return Results.Ok(result);
            }
            return Results.BadRequest(result);
        }
        public static async Task<IResult> GetBrandGrid([FromBody] GetBrandGridDTOQuery data, ISender sender)
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
