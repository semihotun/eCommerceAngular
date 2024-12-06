using eCommerceBase.Domain.SeedWork;

namespace eCommerceBase.Domain.AggregateModels
{
    public class CustomerActivationCode : BaseEntity
    {
        public Guid CustomerUserId { get; private set; }
        public Guid ActivationCode { get; private set; }
        public DateTime ValidtiyDate { get; private set; }

        public CustomerActivationCode(Guid customerUserId, Guid activationCode, DateTime validtiyDate)
        {
            CustomerUserId = customerUserId;
            ActivationCode = activationCode;
            ValidtiyDate = validtiyDate;
        }
        [SwaggerIgnore]
        public CustomerUser? CustomerUser { get; private set; } 
    }
}
