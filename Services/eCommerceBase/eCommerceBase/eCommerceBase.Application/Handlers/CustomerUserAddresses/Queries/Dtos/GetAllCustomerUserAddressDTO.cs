namespace eCommerceBase.Application.Handlers.CustomerUserAddresses.Queries.Dtos;
public class GetAllCustomerUserAddressDTO
{
    public Guid Id { get; set; }
    public string? Name { get; set; }
    public string? DistrictName { get; set; }
    public string? CityName { get; set; }
    public string? Street { get; set; }
    public string? Address { get; set; }
}