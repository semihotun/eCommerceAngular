namespace eCommerceBase.Insfrastructure.Utilities.Security.Jwt
{
    public class ClaimTypeValue
    {
        public string ClaimType { get; set; }
        public string Value { get; set; }

        public ClaimTypeValue(string claimType, string value)
        {
            ClaimType = claimType;
            Value = value;
        }
    }
}
