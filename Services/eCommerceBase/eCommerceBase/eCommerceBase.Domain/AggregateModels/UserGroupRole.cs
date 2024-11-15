using eCommerceBase.Domain.SeedWork;

namespace eCommerceBase.Domain.AggregateModels
{
    public class UserGroupRole : BaseEntity
    {
        public Guid UserGroupId { get; private set; }
        public Guid RoleId { get; private set; }
        [SwaggerIgnore]
        public UserGroup? UserGroup { get; private set; }
        [SwaggerIgnore]
        public Role? Role { get; private set; }

        public UserGroupRole(Guid userGroupId, Guid roleId)
        {
            UserGroupId = userGroupId;
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