using eCommerceBase.Application.Handlers.UserGroupRoles.Commands;
using eCommerceBase.Domain.AggregateModels;
using Riok.Mapperly.Abstractions;

namespace eCommerceBase.Application.Handlers.Mapper
{
    [Mapper]
    public static partial class UserGroupRoleMapper
    {
        public static partial UserGroupRole CreateUserGroupRoleCommandToUserGroupRole(CreateUserGroupRoleCommand userGroupRole);
        public static partial UserGroupRole UpdateUserGroupRoleCommandToUserGroupRole(UpdateUserGroupRoleCommand userGroupRole);
    }
}