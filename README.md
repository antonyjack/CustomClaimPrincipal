# What is ClaimsPrincipal object?

> The claimsprincipal object expose collection of claims. Claims can contain information about the user, roles or permissions, useful to build flexible authorization model.

# How to create custom claimsprincipal?

```C#
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
```

# How to bind custom claimsprinciple in current thread?

```C#
    public class MvcApplication : System.Web.HttpApplication
    {
        .....

        public override void Init()
        {
            this.PostAuthenticateRequest += MvcApplication_PostAuthenticateRequest;
            base.Init();
        }

        void MvcApplication_PostAuthenticateRequest(object sender, EventArgs e)
        {
            if (Request.IsAuthenticated)
            {
                HttpCookie authCookie = Request.Cookies[FormsAuthentication.FormsCookieName];
                if (authCookie != null)
                {
                    FormsAuthenticationTicket authTicket = FormsAuthentication.Decrypt(authCookie.Value);
                    var CustomClaimPrincipal = new CustomClaimPrincipal(authTicket.Name);
                    Context.User = Thread.CurrentPrincipal = CustomClaimPrincipal.GenerateClaimesPrincipal();
                }
            }
        }
    }
```

# How to retrieve claims information?

```C#
            ClaimsPrincipal principal = User as ClaimsPrincipal;
            string Name = principal.Claims.Where(x => x.Type == ClaimTypes.Name).Select(x => x.Value).FirstOrDefault();
            string FullName = principal.Claims
                .Where(x => x.Type == "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/fullname").Select(x => x.Value).FirstOrDefault();
```
