using eCommerceBase.Domain.SeedWork;

namespace eCommerceBase.Domain.AggregateModels
{
    public class UserGroup : BaseEntity
    {
        public string Name { get; private set; }

        public UserGroup(string name)
        {
            Name = name;
        }
        [SwaggerIgnore]
        public ICollection<AdminUser> AdminUserList { get; private set; } = [];
        [SwaggerIgnore]
        public ICollection<CustomerUser> CustomerUserList { get; private set; } = [];
        [SwaggerIgnore]
        public ICollection<UserGroupRole> UserGroupRoleList { get; private set; } = [];

        public void SetId(Guid id)
        {
            Id = id;
        }

        public void AddAdminUserList(AdminUser? adminUser)
        {
            if (adminUser != null)
            {
                AdminUserList.Add(adminUser);
            }
        }

        public void AddCustomerUserList(CustomerUser? customerUser)
        {
            if (customerUser != null)
            {
                CustomerUserList.Add(customerUser);
            }
        }

        public void AddUserGroupRoleList(UserGroupRole? userGroupRole)
        {
            if (userGroupRole != null)
            {
                UserGroupRoleList.Add(userGroupRole);
            }
        }
    }
}