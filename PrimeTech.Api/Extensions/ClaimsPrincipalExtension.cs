using System;
using System.Security.Claims;

namespace PrimeTech.Api.Extensions
{
    public static class ClaimsPrincipalExtension
    {
        public static string GetUserId(this ClaimsPrincipal principal)
        {
            if (principal == null)
                throw new ArgumentNullException(nameof(principal));

            return principal.FindFirst(x => x.Type == ClaimTypes.NameIdentifier).Value;
        }
    }
}
