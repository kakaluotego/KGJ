using System.Threading.Tasks;
using KGJ.Configuration.Dto;

namespace KGJ.Configuration
{
    public interface IConfigurationAppService
    {
        Task ChangeUiTheme(ChangeUiThemeInput input);
    }
}
