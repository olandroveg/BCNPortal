using Microsoft.AspNetCore.Identity;
using System.Security.Claims;

namespace BCNPortal.Utility
{
    public static class ClaimsPrincipalExtensions
    {
        public static bool HasRoleAdmin(this ClaimsPrincipal principal)
        {
           var user = principal.Claims.FirstOrDefault(x => x.Type == ClaimTypes.Name)?.Value;
            var accessUser = StaticConfigurationManager.AppSetting["AccessUsers:admin"];
            return user == accessUser;
           
        }
        public static bool HasRoleBcNode(this ClaimsPrincipal principal)
        {
            var user = principal.Claims.FirstOrDefault(x => x.Type == ClaimTypes.Name)?.Value;
            var accessUser = StaticConfigurationManager.AppSetting["AccessUsers:bcNode"];
            return user == accessUser;
            
        }
        public static Guid GetUserId (this ClaimsPrincipal principal)
        {
            return Guid.Parse(principal.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier)?.Value);
        }

    }
}
