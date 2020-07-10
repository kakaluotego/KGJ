using System.Threading.Tasks;
using Abp.Application.Services;
using KGJ.Sessions.Dto;

namespace KGJ.Sessions
{
    public interface ISessionAppService : IApplicationService
    {
        Task<GetCurrentLoginInformationsOutput> GetCurrentLoginInformations();
    }
}
