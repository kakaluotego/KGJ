using Abp.AspNetCore.Mvc.Controllers;
using Abp.IdentityFramework;
using Microsoft.AspNetCore.Identity;

namespace KGJ.Controllers
{
    public abstract class KGJControllerBase: AbpController
    {
        protected KGJControllerBase()
        {
            LocalizationSourceName = KGJConsts.LocalizationSourceName;
        }

        protected void CheckErrors(IdentityResult identityResult)
        {
            identityResult.CheckErrors(LocalizationManager);
        }
    }
}
