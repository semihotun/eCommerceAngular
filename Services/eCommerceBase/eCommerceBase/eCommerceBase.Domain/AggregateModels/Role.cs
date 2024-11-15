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
        public new Role SetId(Guid id)
        {
            Id = id;
            return this;
        }
        [SwaggerIgnore]
        public ICollection<UserGroup> UserGroupList { get; private set; } = []; 
    }
}