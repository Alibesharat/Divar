using System.Security.Claims;

namespace divar.Extensions
{
     public static class ClaimsPrincipalExtensions
    {
        public static string GetPhoneNumber(this ClaimsPrincipal user)
        {
            return user.FindFirst(ClaimTypes.MobilePhone)?.Value;
        }

        public static string GetFullName(this ClaimsPrincipal user)
        {
            return user.FindFirst(ClaimTypes.Name)?.Value;
        }

        // Add more methods for other claims as needed
    }
}