namespace eCommerceBase.Application.Handlers.ProductComments.Queries.Dtos;
public class GetAllProductCommentApprovedByProductIdDto
{
    public Guid Id { get; set; }
    public bool? IsApprove { get; set; }
    public string? CustomerUserFirstName { get; set; }
    public string? CustomerUserLastName { get; set; }
    public string? Comment { get; set; }
    public DateTime? CreatedDate { get; set; }
}