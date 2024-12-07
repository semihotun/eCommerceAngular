namespace eCommerceBase.Application.Handlers.CustomerUsers.Queries.Dtos
{
    public class CustomerUserDTO
    {
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? Email { get;  set; }
        public DateTime CreatedOnUtc { get; set; }
        public bool IsActivationApprove { get; set; }
    }
}
