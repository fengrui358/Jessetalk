using System;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;

namespace JwtAuthSample
{
    public class MyTokenValidator : ISecurityTokenValidator
    {
        public bool CanReadToken(string securityToken)
        {
            if (!string.IsNullOrEmpty(securityToken))
            {
                return true;
            }

            return false;
        }

        public ClaimsPrincipal ValidateToken(string securityToken, TokenValidationParameters validationParameters,
            out SecurityToken validatedToken)
        {
            validatedToken = null;
            var identity = new ClaimsIdentity(JwtBearerDefaults.AuthenticationScheme);

            if (securityToken.Equals("abcdefg", StringComparison.OrdinalIgnoreCase))
            {
                
                identity.AddClaim(new Claim("name", "free"));
                identity.AddClaim(new Claim(ClaimsIdentity.DefaultRoleClaimType, "SuperAdmin"));
            }

            var principal = new ClaimsPrincipal(identity);

            return principal;
        }

        public bool CanValidateToken => true;

        public int MaximumTokenSizeInBytes { get; set; }
    }
}
