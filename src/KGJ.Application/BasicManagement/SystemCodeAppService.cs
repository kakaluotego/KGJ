/*
 * Copyright © 2020，CloudRoam.com
 * All rights reserved.
 *  
 * 文件名称：SystemCodeAppService.cs
 /* 摘   要：
 *  
 * 当前版本：1.0
 * 作   者：Kakaluote
 */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Threading.Tasks;
using Abp.Application.Services.Dto;
using Abp.Domain.Repositories;
using KGJ.BaseDto;
using KGJ.BasicManagement.Dto;
using Microsoft.EntityFrameworkCore;
using Abp.Linq.Extensions;
using Abp.Runtime.Session;
using KGJ.Authorization.Users;
using Microsoft.EntityFrameworkCore.Query.SqlExpressions;

namespace KGJ.BasicManagement
{
    public class SystemCodeAppService : KGJAppServiceBase,ISystemCodeAppService
    {
        private readonly IRepository<SystemCodeGroup, long> _systemCodeGroupRepository;
        private readonly IRepository<SystemCode, long> _systemCodeRepository;

        public SystemCodeAppService(
            IRepository<SystemCodeGroup,long> systemCodeGroupRepository,
            IRepository<SystemCode,long> systemCodeRepository
            )
        {
            _systemCodeGroupRepository = systemCodeGroupRepository;
            _systemCodeRepository = systemCodeRepository;
        }

        public async Task<User> TestAsync()
        {
            var user = await GetCurrentUserAsync();
            return user;

        }
        /// <summary>
        /// 新增数据字典（含子表）
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<BaseResultDto> CreateSystemCodeGroupAsync(CreateSystemCodeGroupInput input)
        {
            var result=new BaseResultDto();
            //检测GroupNo有没有重复的
            var count = _systemCodeGroupRepository.GetAll().Where(p => p.GroupNo == input.GroupNo && p.IsValid).Count();
            if (count > 0)
            {
                result.IsSuccess = false;
                result.ErrorCode = 250;
                result.ErrorMessage = "GroupNo已存在！";
                return result;
            }
            var entitySystemCodeGroup=new SystemCodeGroup();
            entitySystemCodeGroup.TenantId = AbpSession.TenantId;
            entitySystemCodeGroup.GroupNo = input.GroupNo;
            entitySystemCodeGroup.GroupName = input.GroupName;
            entitySystemCodeGroup.Desc = input.Desc;
            entitySystemCodeGroup.IsValid = input.IsValid;
            entitySystemCodeGroup.CreatorUserId = input.CreatorUserId;

            var groupId = await _systemCodeGroupRepository.InsertAndGetIdAsync(entitySystemCodeGroup);
            if (input.SystemCodeDtos.Count > 0)
            {
                foreach (var item in input.SystemCodeDtos)
                {
                    var entitySystemCode = ObjectMapper.Map<SystemCode>(item);
                    entitySystemCode.GroupId = groupId;
                    entitySystemCode.TenantId = AbpSession.TenantId;
                    await _systemCodeRepository.InsertAsync(entitySystemCode);
                }
            }

            result.IsSuccess = true;
            return result;
        }
        /// <summary>
        /// 更新数据字典(含勾选是否有效)
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<BaseResultDto> UpdateSystemCodeGroupAsync(UpdateSystemCodeGroupInput input)
        {
            var result = new BaseResultDto();
            var entitySystemCodeGroup = await _systemCodeGroupRepository.GetAsync(input.Id);
            entitySystemCodeGroup.GroupNo = input.GroupNo;
            entitySystemCodeGroup.GroupName = input.GroupName;
            entitySystemCodeGroup.Desc = input.Desc;
            entitySystemCodeGroup.IsValid = input.IsValid;
            entitySystemCodeGroup.LastModifierUserId = input.LastModifierUserId;
            await _systemCodeGroupRepository.UpdateAsync(entitySystemCodeGroup);

            if (input.SystemCodeListDtos.Count > 0)
            {
                var entityList = await _systemCodeRepository.GetAllListAsync(p => p.GroupId == input.Id);
                foreach (var item in input.SystemCodeListDtos)
                {
                    //如果是修改子表
                    if (item.Id.HasValue && item.Id.Value > 0)
                    {
                        var entitySystemCode = entityList.Find(p => p.Id == item.Id.Value);
                        if (entitySystemCode == null)
                        {
                            result.IsSuccess = false;
                            result.ErrorCode = 250;
                            result.ErrorMessage = "找不到匹配的数据！";
                            return result;
                        }

                        entitySystemCode.OrderNo = item.OrderNo;
                        entitySystemCode.Key = item.Key;
                        entitySystemCode.Value = item.Value;
                        entitySystemCode.Desc = item.Desc;
                        entitySystemCode.IsValid = item.IsValid;
                        await _systemCodeRepository.UpdateAsync(entitySystemCode);
                    }
                    //如果是新增
                    else
                    {
                        var entitySystemCode = ObjectMapper.Map<SystemCode>(item);
                        entitySystemCode.GroupId = input.Id;
                        entitySystemCode.TenantId = AbpSession.TenantId;
                        await _systemCodeRepository.InsertAsync(entitySystemCode);
                    }
                }
            }

            result.IsSuccess = true;
            return result;
        }
        /// <summary>
        /// 列表页删除
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task DeleteSystemCodeGroupAsync(EntityDto<long> input)
        {
            var entitySystemCodeGroup = await _systemCodeGroupRepository.GetAsync(input.Id);
            entitySystemCodeGroup.DeleterUserId = AbpSession.GetUserId();
            await _systemCodeGroupRepository.DeleteAsync(entitySystemCodeGroup);
        }
        /// <summary>
        /// 列表页查询（字典组）
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<PagedResultDto<SystemCodeGroupListDto>> GetSystemCodeGroupAsync(GetSystemCodeGroupInput input)
        {
            var query = from codeGroup in _systemCodeGroupRepository.GetAll()
                join user in UserManager.Users on codeGroup.CreatorUserId.Value equals user.Id
                where (string.IsNullOrEmpty(input.Filter) ? 1==1 : codeGroup.GroupNo.Contains(input.Filter) || codeGroup.GroupName.Contains(input.Filter)) &&
                codeGroup.IsValid
                select new SystemCodeGroupListDto
                {
                    Id = codeGroup.Id,
                    GroupNo = codeGroup.GroupNo,
                    GroupName = codeGroup.GroupName,
                    Desc = codeGroup.Desc,
                    IsValid = codeGroup.IsValid,
                    CreationTime = codeGroup.CreationTime,
                    CreatorUserId = codeGroup.CreatorUserId,
                    CreatorUserName = user.Surname + " " + user.Name
                };

            var totalCount = await query.CountAsync();
            var items = await query.OrderBy(input.Sorting).PageBy(input).ToListAsync();

            return new PagedResultDto<SystemCodeGroupListDto>(totalCount,items);

        }
        /// <summary>
        /// 获取字典明细
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<ListResultDto<SystemCodeListDto>> GetSystemCodeAsync(EntityDto<long> input)
        {
            var query = from code in _systemCodeRepository.GetAll()
                where code.GroupId == input.Id
                orderby code.OrderNo
                select new SystemCodeListDto
                {
                    Id = code.Id,
                    GroupId = code.GroupId,
                    OrderNo = code.OrderNo,
                    Key = code.Key,
                    Value = code.Value,
                    Desc = code.Desc,
                    IsValid = code.IsValid
                };

            return new ListResultDto<SystemCodeListDto>(await query.ToListAsync());
        }

    }
}
