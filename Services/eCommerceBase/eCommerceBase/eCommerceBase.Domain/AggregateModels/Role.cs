using eCommerceBase.Domain.SeedWork;

namespace eCommerceBase.Domain.AggregateModels
{
    public class Role : BaseEntity
    {
        public string RoleName { get; private set; }
        public Role(string roleName)
        {
            RoleName = roleName;
        }
        public ICollection<UserGroup> UserGroupList { get; private set; } = []; 
    }
}