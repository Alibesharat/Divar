using System.ComponentModel.DataAnnotations;
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

    }

     public static class EnumExtensions
    {
        public static string GetDisplayName(this Enum value)
        {
            var displayAttribute = value.GetType()
                .GetMember(value.ToString())
                .FirstOrDefault()?
                .GetCustomAttributes(typeof(DisplayAttribute), false)
                .FirstOrDefault() as DisplayAttribute;

            return displayAttribute?.Name ?? value.ToString(); // Return display name or the enum's name if not found
        }
    }
}