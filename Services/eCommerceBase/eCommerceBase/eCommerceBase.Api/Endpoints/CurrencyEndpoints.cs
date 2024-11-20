using Carter;
using eCommerceBase.Application.Handlers.Currencies.Commands;
using eCommerceBase.Application.Handlers.Currencies.Queries;
using eCommerceBase.Application.Handlers.Currencies.Queries.Dtos;
using eCommerceBase.Domain.AggregateModels;
using eCommerceBase.Domain.Result;
using eCommerceBase.Insfrastructure.Utilities.Grid.PagedList;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace eCommerceBase.Api.Endpoints
{
    public class CurrencyEndpoints : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            var group = app.MapGroup("api/currency");

            group.MapPost("/create", CreateCurrency)
                .Produces(StatusCodes.Status200OK, typeof(Result));
            group.MapPost("/update", UpdateCurrency)
                .Produces(StatusCodes.Status200OK, typeof(Result))
                .AllowAnonymous();
            group.MapPost("/delete", DeleteCurrency)
                .Produces(StatusCodes.Status200OK, typeof(Result))
                .AllowAnonymous();
            group.MapGet("/getbyid", GetCurrencyById)
               .Produces(StatusCodes.Status200OK, typeof(Result<Currency>))
               .AllowAnonymous();
            group.MapGet("/getall", GetAllCurrency)
                .Produces(StatusCodes.Status200OK, typeof(Result<Currency>))
                .AllowAnonymous();
            group.MapPost("/getgrid", GetCurrencyGrid)
                .Produces(StatusCodes.Status200OK, typeof(Result<IPagedList<CurrencyGridDTO>>))
                .AllowAnonymous();
        }
        public static async Task<IResult> CreateCurrency([FromBody] CreateCurrencyCommand data, ISender sender)
        {
            var result = await sender.Send(data);
            if (result.Success)
            {
                return Results.Ok(result);
            }
            return Results.BadRequest(result);
        }
        public static async Task<IResult> UpdateCurrency([FromBody] UpdateCurrencyCommand data, ISender sender)
        {
            var result = await sender.Send(data);
            if (result.Success)
            {
                return Results.Ok(result);
            }
            return Results.BadRequest(result);
        }
        public static async Task<IResult> DeleteCurrency([FromBody] DeleteCurrencyCommand data, ISender sender)
        {
            var result = await sender.Send(data);
            if (result.Success)
            {
                return Results.Ok(result);
            }
            return Results.BadRequest(result);
        }
        public static async Task<IResult> GetCurrencyById([FromQuery] Guid id, ISender sender)
        {
            var result = await sender.Send(new GetCurrencyByIdQuery(id));
            if (result.Success)
            {
                return Results.Ok(result);
            }
            return Results.BadRequest(result);
        }
        public static async Task<IResult> GetAllCurrency(ISender sender)
        {
            var result = await sender.Send(new GetAllCurrency());
            if (result.Success)
            {
                return Results.Ok(result);
            }
            return Results.BadRequest(result);
        }
        public static async Task<IResult> GetCurrencyGrid([FromBody] GetCurrencyGridDTOQuery data, ISender sender)
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
