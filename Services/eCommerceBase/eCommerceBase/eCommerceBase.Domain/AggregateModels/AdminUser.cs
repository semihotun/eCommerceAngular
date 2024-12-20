using eCommerceBase.Domain.Constant;
using eCommerceBase.Domain.SeedWork;

namespace eCommerceBase.Domain.AggregateModels
{
    public class AdminUser : BaseEntity, IUser
    {
        public AdminUser(string firstName, string lastName, string email, byte[] passwordSalt, byte[] passwordHash, bool status)
        {
            FirstName = firstName;
            LastName = lastName;
            Email = email;
            PasswordSalt = passwordSalt;
            PasswordHash = passwordHash;
            Status = status;
        }

        public string FirstName { get; private set; }
        public string LastName { get; private set; }
        public string Email { get; private set; }
        public byte[] PasswordSalt { get; private set; }
        public byte[] PasswordHash { get; private set; }
        public bool Status { get; private set; }
        public Guid UserGroupId { get; private set; }
        [SwaggerIgnore]
        public UserGroup? UserGroup { get; private set; }
        public void SetAdminForUserGroup()
        {
            UserGroupId = Guid.Parse(InitConst.AdminGuid);
        }
    }
}