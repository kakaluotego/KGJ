using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using KGJ.OrganizationUnits.Dto;
using KGJ.Roles.Dto;

namespace KGJ.OrganizationUnits
{
    public interface IOrganizationUnitAppService :IApplicationService
    {
        Task<ListResultDto<OrganizationUnitTreeDto>> GetOrganizationUnitsAsync();
        Task<PagedResultDto<OrganizationUnitUserListDto>> GetOrganizationUnitUsersAsync(GetOrganizationUnitUsersInput input);
        Task<OrganizationUnitDto> CreateOrganizationUnitAsync(CreateOrganizationUnitInput input);
        Task<OrganizationUnitDto> UpdateOrganizationUnitAsync(UpdateOrganizationUnitInput input);
        Task<OrganizationUnitDto> MoveOrganizationUnitAsync(MoveOrganizationUnitInput input);
        Task DeleteOrganizationUnitAsync(EntityDto<long> input);
        Task RemoveUserFromOrganizationUnitAsync(UserToOrganizationUnitInput input);
        Task AddUsersToOrganizationUnitAsync(UsersToOrganizationUnitInput input);
        Task<PagedResultDto<NameValueDto>> FindUsersAsync(FindOrganizationUnitUsersInput input);


    }
}
