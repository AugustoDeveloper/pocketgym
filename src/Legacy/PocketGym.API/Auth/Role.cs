using System;
using System.Security.Claims;

namespace PocketGym.API.Auth
{
    public class Role
    {
        private static Lazy<Role> lazyUserRole = new Lazy<Role>(() => new Role(nameof(User)));
        private static Lazy<Role> lazyAdminRole = new Lazy<Role>(() => new Role(nameof(Admin)));
        public static Role User => lazyUserRole.Value;
        public static Role Admin => lazyAdminRole.Value;

        public string RoleName { get; }

        public Role(string roleName)
        {
            RoleName = roleName;
        }

        public bool IsAllowedWithCurrentRole(string userIdentity, ClaimsPrincipal currentUser)
            => string.Equals(currentUser.Identity.Name, userIdentity) && currentUser.IsInRole(RoleName);
    }
}