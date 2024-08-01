using Carter;
using eCommerceBase.Application.Handlers.Pages.Commands;
using eCommerceBase.Application.Handlers.Pages.Queries;
using eCommerceBase.Application.Handlers.Pages.Queries.Dtos;
using eCommerceBase.Domain.AggregateModels;
using eCommerceBase.Domain.Result;
using eCommerceBase.Insfrastructure.Utilities.Grid.PagedList;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace eCommerceBase.Api.Endpoints
{
    public class PageEndpoints : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            var group = app.MapGroup("api/page");

            group.MapPost("/create", CreatePage)
                .Produces(StatusCodes.Status200OK, typeof(Result))
                .DisableAntiforgery()
                .AllowAnonymous();
            group.MapPost("/update", UpdatePage)
                .Produces(StatusCodes.Status200OK, typeof(Result))
                .AllowAnonymous();
            group.MapPost("/delete", DeletePage)
                .Produces(StatusCodes.Status200OK, typeof(Result))
                .AllowAnonymous();
            group.MapGet("/getbyid", GetPageById)
               .Produces(StatusCodes.Status200OK, typeof(Result<Page>))
               .AllowAnonymous();
            group.MapGet("/getall", GetAllPage)
                .Produces(StatusCodes.Status200OK, typeof(Result<Page>))
                .AllowAnonymous();
            group.MapPost("/getgrid", GetPageGrid)
                .Produces(StatusCodes.Status200OK, typeof(Result<IPagedList<PageGridDTO>>))
                .AllowAnonymous();
        }
        public static async Task<IResult> CreatePage([FromBody] CreatePageCommand data, ISender sender)
        {
            var result = await sender.Send(data);
            if (result.Success)
            {
                return Results.Ok(result);
            }
            return Results.BadRequest(result);
        }
        public static async Task<IResult> UpdatePage([FromBody] UpdatePageCommand data, ISender sender)
        {
            var result = await sender.Send(data);
            if (result.Success)
            {
                return Results.Ok(result);
            }
            return Results.BadRequest(result);
        }
        public static async Task<IResult> DeletePage([FromBody] DeletePageCommand data, ISender sender)
        {
            var result = await sender.Send(data);
            if (result.Success)
            {
                return Results.Ok(result);
            }
            return Results.BadRequest(result);
        }
        public static async Task<IResult> GetPageById([FromQuery] Guid id, ISender sender)
        {
            var result = await sender.Send(new GetPageByIdQuery(id));
            if (result.Success)
            {
                return Results.Ok(result);
            }
            return Results.BadRequest(result);
        }
        public static async Task<IResult> GetAllPage(ISender sender)
        {
            var result = await sender.Send(new GetAllPage());
            if (result.Success)
            {
                return Results.Ok(result);
            }
            return Results.BadRequest(result);
        }
        public static async Task<IResult> GetPageGrid([FromBody] GetPageGridDTOQuery data, ISender sender)
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

