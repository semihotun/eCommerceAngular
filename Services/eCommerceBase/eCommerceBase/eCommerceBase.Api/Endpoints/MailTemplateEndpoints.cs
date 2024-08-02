using Carter;
using eCommerceBase.Application.Handlers.MailTemplates.Commands;
using eCommerceBase.Application.Handlers.MailTemplates.Queries;
using eCommerceBase.Application.Handlers.MailTemplates.Queries.Dtos;
using eCommerceBase.Domain.Result;
using eCommerceBase.Insfrastructure.Utilities.Grid.PagedList;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace eCommerceBase.Api.Endpoints
{
    public class MailTemplateEndpoints : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            var group = app.MapGroup("api/mailtemplate");

            group.MapPost("/update", UpdateMailTemplate)
                .Produces(StatusCodes.Status200OK, typeof(Result))
                .AllowAnonymous();
            group.MapGet("/getbyid", GetMailTemplateById)
               .Produces(StatusCodes.Status200OK, typeof(Result<MailTemplateByIdDTO>))
               .AllowAnonymous();
            group.MapPost("/getgrid", GetMailTemplateGrid)
               .Produces(StatusCodes.Status200OK, typeof(Result<IPagedList<MailTemplateGridDTO>>))
               .AllowAnonymous();
        }
        public static async Task<IResult> UpdateMailTemplate([FromBody] UpdateMailTemplateCommand data, ISender sender)
        {
            var result = await sender.Send(data);
            if (result.Success)
            {
                return Results.Ok(result);
            }
            return Results.BadRequest(result);
        }
        public static async Task<IResult> GetMailTemplateById([FromQuery] Guid id, ISender sender)
        {
            var result = await sender.Send(new GetMailTemplateByIdDTOQuery(id));
            if (result.Success)
            {
                return Results.Ok(result);
            }
            return Results.BadRequest(result);
        }
        public static async Task<IResult> GetMailTemplateGrid([FromBody] GetMailTemplateGridDTOQuery data, ISender sender)
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
