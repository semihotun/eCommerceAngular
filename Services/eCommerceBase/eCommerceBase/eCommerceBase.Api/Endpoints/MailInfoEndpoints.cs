using Carter;
using eCommerceBase.Application.Handlers.MailInfos.Commands;
using eCommerceBase.Application.Handlers.MailInfos.Queries;
using eCommerceBase.Domain.AggregateModels;
using eCommerceBase.Domain.Result;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace eCommerceBase.Api.Endpoints;

public class MailInfoEndpoints : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("api/mailInfo");

        group.MapPost("/update", UpdateMailInfo)
                     .Produces(StatusCodes.Status200OK, typeof(Result));
        group.MapGet("/get", GetMailInfo)
           .Produces(StatusCodes.Status200OK, typeof(Result<MailInfo>))
           .AllowAnonymous();
    }

    public static async Task<IResult> UpdateMailInfo([FromBody] UpdateMailInfoCommand data, ISender sender)
    {
        var result = await sender.Send(data);
        if (result.Success)
        {
            return Results.Ok(result);
        }
        return Results.BadRequest(result);
    }
    public static async Task<IResult> GetMailInfo(ISender sender)
    {
        var result = await sender.Send(new GetMailInfoQuery());
        if (result.Success)
        {
            return Results.Ok(result);
        }
        return Results.BadRequest(result);
    }
}
