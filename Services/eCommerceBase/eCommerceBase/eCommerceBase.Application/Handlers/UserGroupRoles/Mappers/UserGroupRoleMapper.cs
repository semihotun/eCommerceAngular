using Riok.Mapperly.Abstractions;
using eCommerceBase.Application.Handlers.UserGroupRoles.Commands;
using eCommerceBase.Domain.AggregateModels;
using eCommerceBase.Application.Handlers.Mapper;

namespace eCommerceBase.Application.Handlers.Mapper
{
    [Mapper]
    public static partial class UserGroupRoleMapper
    {
        public static partial UserGroupRole CreateUserGroupRoleCommandToUserGroupRole(CreateUserGroupRoleCommand userGroupRole);
        public static partial UserGroupRole UpdateUserGroupRoleCommandToUserGroupRole(UpdateUserGroupRoleCommand userGroupRole);
    }
}