using Riok.Mapperly.Abstractions;
using eCommerceBase.Application.Handlers.UserGroups.Commands;
using eCommerceBase.Domain.AggregateModels;
using eCommerceBase.Application.Handlers.Mapper;

namespace eCommerceBase.Application.Handlers.Mapper
{
    [Mapper]
    public static partial class UserGroupMapper
    {
        public static partial UserGroup CreateUserGroupCommandToUserGroup(CreateUserGroupCommand userGroup);
        public static partial UserGroup UpdateUserGroupCommandToUserGroup(UpdateUserGroupCommand userGroup);
    }
}