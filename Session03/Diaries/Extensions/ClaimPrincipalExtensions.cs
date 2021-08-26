using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using IdentityModel;

namespace Diaries.Extensions
{
    public static class ClaimPrincipalExtensions
    {
        public static int GetUserId(this ClaimsPrincipal principal)
        {
            var value = principal.Claims.First(a => a.Type == ClaimTypes.NameIdentifier).Value;
            return int.Parse(value);
        }
    }
}
