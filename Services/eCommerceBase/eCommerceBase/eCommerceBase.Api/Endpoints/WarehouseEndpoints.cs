using Carter;
using eCommerceBase.Application.Handlers.Warehouses.Commands;
using eCommerceBase.Application.Handlers.Warehouses.Queries;
using eCommerceBase.Application.Handlers.Warehouses.Queries.Dtos;
using eCommerceBase.Domain.AggregateModels;
using eCommerceBase.Domain.Result;
using eCommerceBase.Insfrastructure.Utilities.Grid.PagedList;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace eCommerceBase.Api.Endpoints
{
    public class WarehouseEndpoints : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            var group = app.MapGroup("api/warehouse");

            group.MapPost("/create", CreateWarehouse)
                .Produces(StatusCodes.Status200OK, typeof(Result));
            group.MapPost("/update", UpdateWarehouse)
                .Produces(StatusCodes.Status200OK, typeof(Result))
                .AllowAnonymous();
            group.MapPost("/delete", DeleteWarehouse)
                .Produces(StatusCodes.Status200OK, typeof(Result))
                .AllowAnonymous();
            group.MapGet("/getbyid", GetWarehouseById)
               .Produces(StatusCodes.Status200OK, typeof(Result<Warehouse>))
               .AllowAnonymous();
            group.MapGet("/getall", GetAllWarehouse)
                .Produces(StatusCodes.Status200OK, typeof(Result<Warehouse>))
                .AllowAnonymous();
            group.MapPost("/getgrid", GetWarehouseGrid)
                .Produces(StatusCodes.Status200OK, typeof(Result<IPagedList<WarehouseGridDTO>>))
                .AllowAnonymous();
        }
        public static async Task<IResult> CreateWarehouse([FromBody] CreateWarehouseCommand data, ISender sender)
        {
            var result = await sender.Send(data);
            if (result.Success)
            {
                return Results.Ok(result);
            }
            return Results.BadRequest(result);
        }
        public static async Task<IResult> UpdateWarehouse([FromBody] UpdateWarehouseCommand data, ISender sender)
        {
            var result = await sender.Send(data);
            if (result.Success)
            {
                return Results.Ok(result);
            }
            return Results.BadRequest(result);
        }
        public static async Task<IResult> DeleteWarehouse([FromBody] DeleteWarehouseCommand data, ISender sender)
        {
            var result = await sender.Send(data);
            if (result.Success)
            {
                return Results.Ok(result);
            }
            return Results.BadRequest(result);
        }
        public static async Task<IResult> GetWarehouseById([FromQuery] Guid id, ISender sender)
        {
            var result = await sender.Send(new GetWarehouseByIdQuery(id));
            if (result.Success)
            {
                return Results.Ok(result);
            }
            return Results.BadRequest(result);
        }
        public static async Task<IResult> GetAllWarehouse(ISender sender)
        {
            var result = await sender.Send(new GetAllWarehouse());
            if (result.Success)
            {
                return Results.Ok(result);
            }
            return Results.BadRequest(result);
        }
        public static async Task<IResult> GetWarehouseGrid([FromBody] GetWarehouseGridDTOQuery data, ISender sender)
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

