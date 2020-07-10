/*
 * Copyright © 2020，CloudRoam.com
 * All rights reserved.
 *  
 * 文件名称：CommonLookupAppService.cs
 /* 摘   要：
 *  
 * 当前版本：1.0
 * 作   者：Kakaluote
 */

using Abp.Application.Services.Dto;
using KGJ.Common.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using Abp.Extensions;
using Abp.Linq.Extensions;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using Abp.Collections.Extensions;

namespace KGJ.Common
{
    public class CommonLookupAppService : KGJAppServiceBase, ICommonLookupAppService
    {

        public async Task<PagedResultDto<NameValueDto>> FindUsers(FindUsersInput input)
        {
            if (AbpSession.TenantId != null)
            {
                input.TenantId = AbpSession.TenantId;
            }

            using (CurrentUnitOfWork.SetTenantId(input.TenantId))
            {
                var query = UserManager.Users
                    .WhereIf(
                        !input.Filter.IsNullOrWhiteSpace(),
                        u =>
                            u.Name.Contains(input.Filter) ||
                            u.Surname.Contains(input.Filter) ||
                            u.UserName.Contains(input.Filter) ||
                            u.EmailAddress.Contains(input.Filter)
                    );
                var userCount = await query.CountAsync();
                var users = await query
                    .OrderBy(u => u.Name)
                    .ThenBy(u => u.Name)
                    .PageBy(input)
                    .ToListAsync();

                return new PagedResultDto<NameValueDto>(
                    userCount,
                    users.Select(u =>
                        new NameValueDto(
                            u.FullName + "(" + u.EmailAddress + ")",
                            u.Id.ToString()
                        )
                    ).ToList()
                );

            }

        }

        public GetDefaultEditionNameOutput GetDefaultEditionName()
        {
            throw new NotImplementedException();
        }
    }
}
