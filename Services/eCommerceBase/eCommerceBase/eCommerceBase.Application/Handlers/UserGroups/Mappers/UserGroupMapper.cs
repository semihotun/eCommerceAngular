using eCommerceBase.Application.Handlers.UserGroups.Commands;
using eCommerceBase.Domain.AggregateModels;
using Riok.Mapperly.Abstractions;

namespace eCommerceBase.Application.Handlers.Mapper
{
    [Mapper]
    public static partial class UserGroupMapper
    {
        public static partial UserGroup CreateUserGroupCommandToUserGroup(CreateUserGroupCommand userGroup);
        public static partial UserGroup UpdateUserGroupCommandToUserGroup(UpdateUserGroupCommand userGroup);
    }
}