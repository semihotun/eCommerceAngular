using eCommerceBase.Insfrastructure.Utilities.ServiceBus;

namespace eCommerceBase.Insfrastructure.Utilities.AdminRole
{
    [UrnType<AddAdminRoleIntegrationEvent>]
    public class AddAdminRoleIntegrationEvent(string[] roleName) : IMessage
    {
        public string[] RoleName { get; set; } = roleName;
    }
}
