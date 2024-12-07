using eCommerceBase.Domain.SeedWork;
using eCommerceBase.Insfrastructure.Utilities.Identity.Extension;
using eCommerceBase.Insfrastructure.Utilities.Security.Encyption;
using eCommerceBase.Insfrastructure.Utilities.Security.Jwt;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace eCommerceBase.Insfrastructure.Utilities.Identity.Service
{
    public class TokenService:ITokenService
    {
        public IConfiguration Configuration { get; }
        private readonly TokenOptions? _tokenOptions;
        private DateTime _accessTokenExpiration;
        public TokenService(IConfiguration configuration)
        {
            Configuration = configuration;
            _tokenOptions = Configuration.GetSection("TokenOptions").Get<TokenOptions>();
        }
        public static string DecodeToken(string input)
        {
            var handler = new JwtSecurityTokenHandler();
            if (input.StartsWith("Bearer "))
                input = input["Bearer ".Length..];
            return handler.ReadJwtToken(input).ToString();
        }
        public AccessToken CreateToken(IUser user, IEnumerable<string>? roleList)
        {
            _accessTokenExpiration = DateTime.Now.AddMinutes(_tokenOptions.AccessTokenExpiration);
            var securityKey = SecurityKeyHelper.CreateSecurityKey(_tokenOptions.SecurityKey);
            var signingCredentials = SigningCredentialsHelper.CreateSigningCredentials(securityKey);
            var jwt = CreateJwtSecurityToken(_tokenOptions, user, signingCredentials, roleList);
            var jwtSecurityTokenHandler = new JwtSecurityTokenHandler();
            var token = jwtSecurityTokenHandler.WriteToken(jwt);
            return new AccessToken(jwt.Claims.Select(x => new ClaimTypeValue(x.Type, x.Value)),token,_accessTokenExpiration);
        }
        public JwtSecurityToken CreateJwtSecurityToken(TokenOptions tokenOptions, IUser user,
                SigningCredentials signingCredentials, IEnumerable<string>? roleList)
        {
            return new JwtSecurityToken(
                    issuer: tokenOptions.Issuer,
                    audience: tokenOptions.Audience,
                    claims: SetClaims(user, roleList),
                    notBefore: DateTime.Now,
                    expires: _accessTokenExpiration,
                    signingCredentials: signingCredentials
            );
        }
        private IEnumerable<Claim> SetClaims(IUser user, IEnumerable<string>? roleList)
        {
            var claims = new List<Claim>
            {
                new("name", user.FirstName),
                new("surname", user.LastName),
                new("id", user.Id.ToString()),
                new("email", user.Id.ToString()),
                new("roles",roleList!.ToArray().ToString()!)
            };
            return claims;
        }
        public bool ValidateToken(string token)
        {
            try
            {
                var jwtSecurityTokenHandler = new JwtSecurityTokenHandler();
                jwtSecurityTokenHandler.ValidateToken(token, new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidIssuer = _tokenOptions?.Issuer,
                    ValidAudience = _tokenOptions.Audience,
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = SecurityKeyHelper.CreateSecurityKey(_tokenOptions.SecurityKey)
                }, out var validateToken);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}
