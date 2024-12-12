using eCommerceBase.Domain.Constant;
using eCommerceBase.Domain.SeedWork;

namespace eCommerceBase.Domain.AggregateModels
{
    public class CustomerUser : BaseEntity, IUser
    {
        public CustomerUser(string firstName, string lastName, string email,
            byte[] passwordSalt, byte[] passwordHash, bool status, bool isActivationApprove)
        {
            FirstName = firstName;
            LastName = lastName;
            Email = email;
            PasswordSalt = passwordSalt;
            PasswordHash = passwordHash;
            Status = status;
            IsActivationApprove = isActivationApprove;
        }

        public string FirstName { get; private set; }
        public string LastName { get; private set; }
        public string Email { get; private set; }
        public byte[] PasswordSalt { get; private set; }
        public byte[] PasswordHash { get; private set; }
        public bool Status { get; private set; }
        public Guid UserGroupId { get; private set; }
        public bool IsActivationApprove { get; private set; }
        public void UpdateCustomerUser(string firstName, string lastName)
        {
            this.FirstName = firstName;
            this.LastName = lastName;
        }
        [SwaggerIgnore]
        public UserGroup? UserGroup { get; private set; }
        public void SetUserForUserGroup()
        {
            UserGroupId = Guid.Parse(InitConst.UserGuid);
        }
        public void SetIsActivationApprove(bool isActivationApprove)
        {
            IsActivationApprove = isActivationApprove;
        }
        [SwaggerIgnore]
        public CustomerActivationCode? CustomerActivationCode { get; private set; }
        [SwaggerIgnore]
        public ICollection<ProductFavorite> ProductFavoriteList { get; private set; } = [];
    }
}
