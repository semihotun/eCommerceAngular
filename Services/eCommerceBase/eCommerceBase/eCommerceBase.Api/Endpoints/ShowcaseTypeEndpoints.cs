using Carter;
using eCommerceBase.Application.Handlers.ShowCaseTypes.Queries;
using eCommerceBase.Domain.AggregateModels;
using eCommerceBase.Domain.Result;
using eCommerceBase.Insfrastructure.Utilities.Grid.PagedList;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace eCommerceBase.Api.Endpoints;
public class ShowcaseTypeEndpoints : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("api/showcasetype");

        group.MapGet("/getall", GetAllShowCase)
            .Produces(StatusCodes.Status200OK, typeof(Result<ShowCase>))
            .AllowAnonymous();
    }
    public static async Task<IResult> GetAllShowCase(ISender sender)
    {
        var result = await sender.Send(new GetAllShowCaseType());
        if (result.Success)
        {
            return Results.Ok(result);
        }
        return Results.BadRequest(result);
    }
}

