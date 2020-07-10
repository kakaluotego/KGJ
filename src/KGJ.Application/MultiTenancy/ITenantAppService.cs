using Abp.Application.Services;
using KGJ.MultiTenancy.Dto;

namespace KGJ.MultiTenancy
{
    public interface ITenantAppService : IAsyncCrudAppService<TenantDto, int, PagedTenantResultRequestDto, CreateTenantDto, TenantDto>
    {
    }
}

