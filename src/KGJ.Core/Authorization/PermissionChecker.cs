using Abp.Authorization;
using KGJ.Authorization.Roles;
using KGJ.Authorization.Users;

namespace KGJ.Authorization
{
    public class PermissionChecker : PermissionChecker<Role, User>
    {
        public PermissionChecker(UserManager userManager)
            : base(userManager)
        {
        }
    }
}
