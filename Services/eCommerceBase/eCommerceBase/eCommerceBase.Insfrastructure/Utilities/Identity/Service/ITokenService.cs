using eCommerceBase.Domain.SeedWork;
using eCommerceBase.Insfrastructure.Utilities.Security.Jwt;

namespace eCommerceBase.Insfrastructure.Utilities.Identity.Service
{
    public interface ITokenService
    {
        AccessToken CreateToken(IUser user,IEnumerable<string>? roleList);
        bool ValidateToken(string token);
    }
}
