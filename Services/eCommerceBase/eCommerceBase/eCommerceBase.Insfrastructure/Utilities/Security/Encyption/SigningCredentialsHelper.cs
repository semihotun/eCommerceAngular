﻿using Microsoft.IdentityModel.Tokens;

namespace eCommerceBase.Insfrastructure.Utilities.Security.Encyption
{
    public static class SigningCredentialsHelper
    {
        public static SigningCredentials CreateSigningCredentials(SecurityKey securityKey)
        {
            return new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256Signature);
        }
    }
}
