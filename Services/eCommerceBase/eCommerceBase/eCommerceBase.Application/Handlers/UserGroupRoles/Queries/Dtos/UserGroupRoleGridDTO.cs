namespace eCommerceBase.Application.Handlers.UserGroupRoles.Queries.Dtos;
public class UserGroupRoleGridDTO
{
    public Guid Id { get; set; }
    public Guid? UserGroupId { get; set; }
    public Guid? RoleId { get; set; }
    public string? RoleRoleName { get; set; }
}