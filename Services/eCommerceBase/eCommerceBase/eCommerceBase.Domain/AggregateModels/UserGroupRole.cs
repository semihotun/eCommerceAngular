using eCommerceBase.Domain.SeedWork;

namespace eCommerceBase.Domain.AggregateModels
{
    public class UserGroupRole : BaseEntity
    {
        public Guid UserGrupId { get; private set; }
        public Guid RoleId { get; private set; }
        public UserGroup? UserGroup { get; private set; }
        public Role? Role { get; private set; }

        public UserGroupRole(Guid userGrupId, Guid roleId)
        {
            UserGrupId = userGrupId;
            RoleId = roleId;
        }

        public void SetUserGroup(UserGroup? userGroup)
        {
            UserGroup = userGroup;
        }

        public void SetRole(Role? role)
        {
            Role = role;
        }
    }
}