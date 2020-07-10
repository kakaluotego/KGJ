using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using KGJ.Common.Dto;

namespace KGJ.Common
{
    public interface ICommonLookupAppService : IApplicationService
    {
        Task<PagedResultDto<NameValueDto>> FindUsers(FindUsersInput input);
        GetDefaultEditionNameOutput GetDefaultEditionName();
    }
}
