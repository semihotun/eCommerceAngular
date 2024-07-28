using Carter;
using eCommerceBase.Application.Handlers.GridSettings.Commands;
using eCommerceBase.Application.Handlers.GridSettings.Queries;
using eCommerceBase.Domain.AggregateModels;
using eCommerceBase.Domain.Result;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.IO;

namespace eCommerceBase.Api.Endpoints
{
    public class GridSettingEndpoints : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            var group = app.MapGroup("api/gridsetting");

            group.MapPost("/creategridsetting", CreateGridSetting)
                .Produces(StatusCodes.Status200OK, typeof(Result))
                .DisableAntiforgery()
                .AllowAnonymous();
            group.MapPost("/updategridsetting", UpdateGridSetting)
                .Produces(StatusCodes.Status200OK, typeof(Result))
                .AllowAnonymous();
            group.MapPost("/deletegridsetting", DeleteGridSetting)
                .Produces(StatusCodes.Status200OK, typeof(Result))
                .AllowAnonymous();
            group.MapGet("/getgridsettingbypath", GetGridSettingByPath)
               .Produces(StatusCodes.Status200OK, typeof(Result<GridSetting>))
               .AllowAnonymous();
            group.MapGet("/getallgridsetting", GetAllGridSetting)
            .Produces(StatusCodes.Status200OK, typeof(Result<GridSetting>))
            .AllowAnonymous();
        }
        public static async Task<IResult> CreateGridSetting([FromBody] CreateGridSettingCommand data, ISender sender)
        {
            var result = await sender.Send(data);
            if (result.Success)
            {
                return Results.Ok(result);
            }
            return Results.BadRequest(result);
        }
        public static async Task<IResult> UpdateGridSetting([FromBody] UpdateGridSettingCommand data, ISender sender)
        {
            var result = await sender.Send(data);
            if (result.Success)
            {
                return Results.Ok(result);
            }
            return Results.BadRequest(result);
        }
        public static async Task<IResult> DeleteGridSetting([FromBody] DeleteGridSettingCommand data, ISender sender)
        {
            var result = await sender.Send(data);
            if (result.Success)
            {
                return Results.Ok(result);
            }
            return Results.BadRequest(result);
        }
        public static async Task<IResult> GetGridSettingByPath([FromQuery] string path, ISender sender)
        {
            var result = await sender.Send(new GetGridSettingByPathQuery(path));
            if (result.Success)
            {
                return Results.Ok(result);
            }
            return Results.BadRequest(result);
        }
        public static async Task<IResult> GetAllGridSetting(ISender sender)
        {
            var result = await sender.Send(new GetAllGridSetting());
            if (result.Success)
            {
                return Results.Ok(result);
            }
            return Results.BadRequest(result);
        }
    }
}
