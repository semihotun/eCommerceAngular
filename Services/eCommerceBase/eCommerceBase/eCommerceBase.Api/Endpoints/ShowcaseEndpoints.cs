using Carter;
using eCommerceBase.Application.Handlers.ShowCases.Commands;
using eCommerceBase.Application.Handlers.ShowCases.Queries;
using eCommerceBase.Application.Handlers.ShowCases.Queries.Dtos;
using eCommerceBase.Domain.AggregateModels;
using eCommerceBase.Domain.Result;
using eCommerceBase.Insfrastructure.Utilities.Grid.PagedList;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace eCommerceBase.Api.Endpoints;
public class ShowcaseEndpoints : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("api/showcase");

        group.MapPost("/create", CreateShowCase)
            .Produces(StatusCodes.Status200OK, typeof(Result));
        group.MapPost("/update", UpdateShowCase)
            .Produces(StatusCodes.Status200OK, typeof(Result))
            .AllowAnonymous();
        group.MapPost("/delete", DeleteShowCase)
            .Produces(StatusCodes.Status200OK, typeof(Result))
            .AllowAnonymous();
        group.MapGet("/getbyid", GetShowCaseById)
           .Produces(StatusCodes.Status200OK, typeof(Result<ShowCase>))
           .AllowAnonymous();
        group.MapGet("/getall", GetAllShowCase)
            .Produces(StatusCodes.Status200OK, typeof(Result<ShowCase>))
            .AllowAnonymous();
        group.MapGet("/getallhome", GetAllHome)
           .Produces(StatusCodes.Status200OK, typeof(Result<AllShowcaseDTO>))
           .AllowAnonymous();
        group.MapPost("/getgrid", GetShowCaseGrid)
            .Produces(StatusCodes.Status200OK, typeof(Result<IPagedList<ShowcaseGrid>>))
            .AllowAnonymous();
    }
    public static async Task<IResult> CreateShowCase([FromBody] CreateShowCaseCommand data, ISender sender)
    {
        var result = await sender.Send(data);
        if (result.Success)
        {
            return Results.Ok(result);
        }
        return Results.BadRequest(result);
    }
    public static async Task<IResult> UpdateShowCase([FromBody] UpdateShowCaseCommand data, ISender sender)
    {
        var result = await sender.Send(data);
        if (result.Success)
        {
            return Results.Ok(result);
        }
        return Results.BadRequest(result);
    }
    public static async Task<IResult> DeleteShowCase([FromBody] DeleteShowCaseCommand data, ISender sender)
    {
        var result = await sender.Send(data);
        if (result.Success)
        {
            return Results.Ok(result);
        }
        return Results.BadRequest(result);
    }
    public static async Task<IResult> GetShowCaseById([FromQuery] Guid id, ISender sender)
    {
        var result = await sender.Send(new GetShowCaseByIdQuery(id));
        if (result.Success)
        {
            return Results.Ok(result);
        }
        return Results.BadRequest(result);
    }
    public static async Task<IResult> GetAllHome(ISender sender)
    {
        var result = await sender.Send(new GetAllShowcaseDTOQuery());
        if (result.Success)
        {
            return Results.Ok(result);
        }
        return Results.BadRequest(result);
    }
    public static async Task<IResult> GetAllShowCase(ISender sender)
    {
        var result = await sender.Send(new GetAllShowCase());
        if (result.Success)
        {
            return Results.Ok(result);
        }
        return Results.BadRequest(result);
    }
    public static async Task<IResult> GetShowCaseGrid([FromBody] GetShowcaseGridQuery data, ISender sender)
    {
        var result = await sender.Send(data);
        if (result.Success)
        {
            return Results.Ok(result);
        }
        return Results.BadRequest(result);
    }
}

