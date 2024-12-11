using Carter;
using eCommerceBase.Application.Handlers.ProductComments.Commands;
using eCommerceBase.Application.Handlers.ProductComments.Queries;
using eCommerceBase.Domain.Result;
using eCommerceBase.Insfrastructure.Utilities.Grid.PagedList;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace eCommerceBase.Api.Endpoints
{
    public class ProductCommentEndpoints : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            var group = app.MapGroup("api/productComment");

            group.MapPost("/create", CreateProductComment)
                .Produces(StatusCodes.Status200OK, typeof(Result));
            group.MapPost("/delete", DeleteProductComment)
                .Produces(StatusCodes.Status200OK, typeof(Result))
                .AllowAnonymous();
            group.MapPost("/getallproductcommentapprovedbyproductiddto", GetAllProductCommentApprovedById)
                .Produces(StatusCodes.Status200OK, typeof(Result<IPagedList<GetAllProductCommentApprovedByIdQuery>>))
                .AllowAnonymous();
            group.MapPost("/approvedproductcomment", ApprovedProductComment)
                .Produces(StatusCodes.Status200OK, typeof(Result));
            group.MapPost("/disapprovedproductcommentcommand", DisapprovedProductCommentCommand)
                .Produces(StatusCodes.Status200OK, typeof(Result));
        }
        public static async Task<IResult> CreateProductComment([FromBody] CreateProductCommentCommand data, ISender sender)
        {
            var result = await sender.Send(data);
            if (result.Success)
            {
                return Results.Ok(result);
            }
            return Results.BadRequest(result);
        }
        public static async Task<IResult> DeleteProductComment([FromBody] DeleteProductCommentCommand data, ISender sender)
        {
            var result = await sender.Send(data);
            if (result.Success)
            {
                return Results.Ok(result);
            }
            return Results.BadRequest(result);
        }
        public static async Task<IResult> GetAllProductCommentApprovedById([FromBody] GetAllProductCommentApprovedByIdQuery data, ISender sender)
        {
            var result = await sender.Send(data);
            if (result.Success)
            {
                return Results.Ok(result);
            }
            return Results.BadRequest(result);
        }
        public static async Task<IResult> ApprovedProductComment([FromBody] ApprovedProductCommentCommand data, ISender sender)
        {
            var result = await sender.Send(data);
            if (result.Success)
            {
                return Results.Ok(result);
            }
            return Results.BadRequest(result);
        }
        public static async Task<IResult> DisapprovedProductCommentCommand([FromBody] DisapprovedProductCommentCommand data, ISender sender)
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
