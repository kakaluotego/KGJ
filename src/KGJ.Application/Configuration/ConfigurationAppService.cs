using System.Threading.Tasks;
using Abp.Authorization;
using Abp.Runtime.Session;
using KGJ.Configuration.Dto;

namespace KGJ.Configuration
{
    [AbpAuthorize]
    public class ConfigurationAppService : KGJAppServiceBase, IConfigurationAppService
    {
        public async Task ChangeUiTheme(ChangeUiThemeInput input)
        {
            await SettingManager.ChangeSettingForUserAsync(AbpSession.ToUserIdentifier(), AppSettingNames.UiTheme, input.Theme);
        }
    }
}
