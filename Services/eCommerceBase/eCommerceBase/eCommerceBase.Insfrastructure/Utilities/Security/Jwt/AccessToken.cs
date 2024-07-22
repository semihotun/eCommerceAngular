namespace eCommerceBase.Insfrastructure.Utilities.Security.Jwt
{
    public class AccessToken : IAccessToken
    {
        public AccessToken(IEnumerable<ClaimTypeValue> claims, string token, DateTime expiration)
        {
            Claims = claims;
            Token = token;
            Expiration = expiration;
        }
        public IEnumerable<ClaimTypeValue> Claims { get; set; }
        public string Token { get; set; }
        public DateTime Expiration { get; set; }
    }
}
