using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using IdentityServer4.Models;
using IdentityServer4.Test;
using Microsoft.Extensions.Logging;

namespace STS.Services
{
    public class TokenExchangeProfileService : TestUserProfileService
    {
        public TokenExchangeProfileService(TestUserStore users, ILogger<TestUserProfileService> logger) : base(users, logger)
        {

        }

        public override Task GetProfileDataAsync(ProfileDataRequestContext context)
        {
            if (context.Subject.HasClaim(a=> a.Type == "act"))
                context.IssuedClaims.Add(context.Subject.FindFirst(a=> a.Type == "act"));

            return base.GetProfileDataAsync(context);
        }
    }
}