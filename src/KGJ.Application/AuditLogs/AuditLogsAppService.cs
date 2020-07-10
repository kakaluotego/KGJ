using Abp.Application.Services.Dto;
using Abp.Auditing;
using Abp.Domain.Repositories;
using KGJ.AuditLogs.Dto;
using KGJ.Authorization.Users;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Abp.Linq;
using System.Linq;
using Abp.Domain.Uow;
using Microsoft.EntityFrameworkCore;
using System.Linq.Dynamic.Core;
using Abp.Extensions;
using Abp.Linq.Extensions;
using KGJ.Authorization;
using Abp.Authorization;
using KGJ.OrganizationUnits;

namespace KGJ.AuditLogs
{
    [AbpAuthorize(PermissionNames.Pages_AuditLogs)]
    public class AuditLogsAppService : KGJAppServiceBase
    {
        private readonly IRepository<AuditLog, long> _auditLogsRepository;
        private readonly UserManager _userManager;
        private readonly IRepository<User, long> _userRepository;

        public AuditLogsAppService(IRepository<AuditLog, long> auditLogsRepository, UserManager userManager, IRepository<User, long> userRepository)
        {
            _auditLogsRepository = auditLogsRepository;
            _userManager = userManager;
            _userRepository = userRepository;
        }

        [AbpAuthorize(PermissionNames.Pages_AuditLogs)]
        public async Task<PagedResultDto<AuditLogDto>> GetAuditLogAsync(GetAuditLogInput input)
        {
            //UnitOfWorkManager.Current.DisableFilter(AbpDataFilters.);
            var query = from log in _auditLogsRepository.GetAll()
                        join user in _userRepository.GetAll() on log.UserId equals user.Id
                        select new AuditLogDto
                        {
                            Id = log.Id,
                            BrowserInfo = log.BrowserInfo,
                            ClientIpAddress = log.ClientIpAddress,
                            ClientName = log.ClientName,
                            CustomData = log.CustomData,
                            Exception = log.Exception,
                            ExecutionDuration = log.ExecutionDuration,
                            ExecutionTime = log.ExecutionTime,
                            MethodName = log.MethodName,
                            Parameters = log.Parameters,
                            ServiceName = log.ServiceName,
                            ReturnValue = log.ReturnValue,
                            CreatorUserName = user.UserName
                        };

            if (!string.IsNullOrEmpty(input.UserName))
            {
                query = query.Where(p => p.CreatorUserName.Contains(input.UserName));
            }
            if (input.From != null)
            {
                query = query.Where(p => p.ExecutionTime >= input.From);
            }
            if (input.To != null)
            {
                query = query.Where(p => p.ExecutionTime <= input.To.Value.AddDays(1));
            }
            if (!string.IsNullOrEmpty(input.Service))
            {
                query = query.Where(p => p.ServiceName.Contains(input.Service));
            }
            if (!string.IsNullOrEmpty(input.MethodName))
            {
                query = query.Where(p => p.MethodName.Contains(input.MethodName));
            }

            var totalCount = await query.CountAsync();
            var items = await query.OrderBy(input.Sorting).PageBy(input).ToListAsync();

            return new PagedResultDto<AuditLogDto>(totalCount, items);
        }
    }
}
