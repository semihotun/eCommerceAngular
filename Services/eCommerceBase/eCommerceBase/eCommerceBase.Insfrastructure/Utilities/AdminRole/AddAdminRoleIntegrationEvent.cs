using eCommerceBase.Insfrastructure.Utilities.ServiceBus;

namespace eCommerceBase.Insfrastructure.Utilities.AdminRole
{
    [UrnType<AddUserRoleIntegrationEvent>]
    public class AddUserRoleIntegrationEvent(string[] roleName) : IMessage
    {
        public string[] RoleName { get; set; } = roleName;
    }
}
