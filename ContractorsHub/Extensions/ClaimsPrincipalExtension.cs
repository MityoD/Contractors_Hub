using System.Security.Claims;

namespace ContractorsHub.Extensions
{
    public static class ClaimsPrincipalExtension
    {
        public static string Id(this ClaimsPrincipal user)
        {
            return user.FindFirstValue(ClaimTypes.NameIdentifier);
        }

        //public static string Name(this ClaimsPrincipal user) //
        //{
        //    return user.FindFirstValue(ClaimTypes.Name);
        //}
    }
}
