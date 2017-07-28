using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Security.Principal;
using System.Web;

namespace Customize.Forms.Authentication.Models
{
    public class CustomClaimPrincipal
    {
        private string Name = null;
        public CustomClaimPrincipal(string name)
        {
            Name = name;
        }

        private List<Claim> GenerateClaimes()
        {
            List<Claim> claims = new List<Claim>();
            claims.Add(new Claim(ClaimTypes.Name, Name));            
            claims.Add(new Claim(ClaimTypes.Authentication, "true"));
            claims.Add(new Claim("http://schemas.xmlsoap.org/ws/2005/05/identity/claims/fullname", "Full Name"));
            return claims;
        }

        private ClaimsIdentity GenerateClaimesIndentity()
        {
            ClaimsIdentity identity = new ClaimsIdentity(GenerateClaimes(), "Forms");
            return identity;
        }

        public ClaimsPrincipal GenerateClaimesPrincipal()
        {
            ClaimsPrincipal principal = new ClaimsPrincipal(GenerateClaimesIndentity());
            return principal;
        }
    }
}