/*
 * Copyright © 2020，CloudRoam.com
 * All rights reserved.
 *  
 * 文件名称：OrganizationAppService.cs
 /* 摘   要：
 *  
 * 当前版本：1.0
 * 作   者：Kakaluote
 */

using Abp.Application.Services.Dto;
using Abp.Authorization.Users;
using Abp.Domain.Repositories;
using Abp.Linq.Extensions;
using Abp.Organizations;
using KGJ.Authorization.Users;
using KGJ.OrganizationUnits.Dto;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Threading.Tasks;
using Abp.Authorization;
using Abp.Extensions;
using JetBrains.Annotations;
using KGJ.Authorization;
using KGJ.Authorization.Roles;
using KGJ.Roles.Dto;

namespace KGJ.OrganizationUnits
{
    [AbpAuthorize(PermissionNames.Pages_OrganizationUnits)]
    public class OrganizationUnitAppService :KGJAppServiceBase,IOrganizationUnitAppService
    {
        private readonly OrganizationUnitManager _organizationUnitManager;
        private readonly IRepository<OrganizationUnit, long> _organizationUnitRepository;
        private readonly IRepository<UserOrganizationUnit, long> _userOrganizationUnitRepository;
        private readonly IRepository<OrganizationUnitRole, long> _organizationUnitRoleRepository;
        private readonly RoleManager _roleManager;


        public OrganizationUnitAppService(
            OrganizationUnitManager organizationUnitManager,
            IRepository<OrganizationUnit,long> organizationUnitRepository,
            IRepository<UserOrganizationUnit,long> userOrganizationUnitRepository,
            IRepository<OrganizationUnitRole,long> organizationUnitRoleRepository,
            RoleManager roleManager
            )
        {
            _organizationUnitManager = organizationUnitManager;
            _organizationUnitRepository = organizationUnitRepository;
            _userOrganizationUnitRepository = userOrganizationUnitRepository;
            _organizationUnitRoleRepository = organizationUnitRoleRepository;
            _roleManager = roleManager;
        }

        public async Task<List<object>> TestAsync()
        {
            var query = from u in _organizationUnitRepository.GetAll()
                join ou in _userOrganizationUnitRepository.GetAll() on u.Id equals ou.OrganizationUnitId into ouJ
                from ou in ouJ.DefaultIfEmpty()
                group ou by new{ou.OrganizationUnitId,u.DisplayName}
                into g
                select new
                {
                    id = g.Key.OrganizationUnitId,
                    name=g.Key.DisplayName,
                    count = g.Count()
                };

                 var list = await query.ToListAsync();

            return new List<object>(list);
        }
            
        /// <summary>
        /// 获取组织结构树
        /// </summary>
        /// <returns></returns>
        public async Task<ListResultDto<OrganizationUnitTreeDto>> GetOrganizationUnitsAsync()
        {
            var organizationUnits = await _organizationUnitRepository.GetAllListAsync();

            var organizationUnitMemberCounts = await _userOrganizationUnitRepository.GetAll()
                .GroupBy(x => x.OrganizationUnitId)
                .Select(p => new
                {
                    organizationUnitId = p.Key,
                    count = p.Count()
                }).ToDictionaryAsync(x => x.organizationUnitId, y => y.count);

            var organizationUnitList = await _organizationUnitManager.FindChildrenAsync(null);

            var organizationUnitTreeList=new List<OrganizationUnitTreeDto>();
            foreach (var item in organizationUnitList)
            {
                var treeDto = ConvertOrganizationUnitToTree(item, organizationUnitMemberCounts);
                organizationUnitTreeList.Add(treeDto);
            }

            return new ListResultDto<OrganizationUnitTreeDto>(organizationUnitTreeList);

        }

        private OrganizationUnitTreeDto ConvertOrganizationUnitToTree(OrganizationUnit item,Dictionary<long,int> memberCounts)
        {
            var organizationUnitTreeDto=new OrganizationUnitTreeDto();
            organizationUnitTreeDto.Id = item.Id;
            organizationUnitTreeDto.Label = item.DisplayName;
            organizationUnitTreeDto.MemberCount = memberCounts.ContainsKey(item.Id) ? memberCounts[item.Id] : 0;
            organizationUnitTreeDto.Children=new List<OrganizationUnitTreeDto>();
            if (item.Children==null)
            {
                return organizationUnitTreeDto;
            }

            foreach (var itemChild in item.Children)
            {
                var childTree = ConvertOrganizationUnitToTree(itemChild,memberCounts);   
                organizationUnitTreeDto.Children.Add(childTree);
            }

            return organizationUnitTreeDto;
        }
        /// <summary>
        /// 单击某组织获取组织成员（传入组织Id）
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<PagedResultDto<OrganizationUnitUserListDto>> GetOrganizationUnitUsersAsync(GetOrganizationUnitUsersInput input)
        {
            var query = from uou in _userOrganizationUnitRepository.GetAll()
                join ou in _organizationUnitRepository.GetAll() on uou.OrganizationUnitId equals ou.Id
                join user in UserManager.Users on uou.UserId equals user.Id
                where uou.OrganizationUnitId == input.Id
                select new {uou, user};

            var totalCount = await query.CountAsync();
            var items = await query.OrderBy(input.Sorting).PageBy(input).ToListAsync();

            return new PagedResultDto<OrganizationUnitUserListDto>(
                totalCount,
                items.Select(item =>
                {
                    var dto = ObjectMapper.Map<OrganizationUnitUserListDto>(item.user);
                    dto.AddedTime = item.uou.CreationTime;
                    return dto;
                }).ToList());
        }
        /// <summary>
        /// 添加根组织，添加子组织
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        
        [AbpAuthorize(PermissionNames.Pages_OrganizationUnits_ManageOrganizationTree)]
        public async Task<OrganizationUnitDto> CreateOrganizationUnitAsync(CreateOrganizationUnitInput input)
        {
            var organizationUnit=new OrganizationUnit(AbpSession.TenantId,input.DisplayName,input.ParentId);

            await _organizationUnitManager.CreateAsync(organizationUnit);
            await CurrentUnitOfWork.SaveChangesAsync();

            return ObjectMapper.Map<OrganizationUnitDto>(organizationUnit);
        }
        /// <summary>
        /// 修改组织名
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [AbpAuthorize(PermissionNames.Pages_OrganizationUnits_ManageOrganizationTree)]
        public async Task<OrganizationUnitDto> UpdateOrganizationUnitAsync(UpdateOrganizationUnitInput input)
        {
            var organizationUnit = await _organizationUnitRepository.GetAsync(input.Id);

            organizationUnit.DisplayName = input.DisplayName;
            await _organizationUnitRepository.UpdateAsync(organizationUnit);

            return await CreateOrganizationUnitDtoAsync(organizationUnit);
        }
        /// <summary>
        /// 移动某组织到新的父组织结点
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [AbpAuthorize(PermissionNames.Pages_OrganizationUnits_ManageOrganizationTree)]
        public async Task<OrganizationUnitDto> MoveOrganizationUnitAsync(MoveOrganizationUnitInput input)
        {
            await _organizationUnitManager.MoveAsync(input.Id, input.NewParentId);

            return await CreateOrganizationUnitDtoAsync(
                await _organizationUnitRepository.GetAsync(input.Id)
                );
        }
        /// <summary>
        /// 删除某组织
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [AbpAuthorize(PermissionNames.Pages_OrganizationUnits_ManageOrganizationTree)]
        public async Task DeleteOrganizationUnitAsync(EntityDto<long> input)
        {
            await _organizationUnitManager.DeleteAsync(input.Id);
        }
        /// <summary>
        /// 从某组织删除一个成员
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [AbpAuthorize(PermissionNames.Pages_OrganizationUnits_ManageMembers)]
        public async Task RemoveUserFromOrganizationUnitAsync(UserToOrganizationUnitInput input)
        {
            await UserManager.RemoveFromOrganizationUnitAsync(input.UserId, input.OrganizationUnitId);
        }
        /// <summary>
        /// 保存成员到某组织（一个或多个）
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [AbpAuthorize(PermissionNames.Pages_OrganizationUnits_ManageMembers)]
        public async Task AddUsersToOrganizationUnitAsync(UsersToOrganizationUnitInput input)
        {
            foreach (var userId in input.UserIds)
            {
                await UserManager.AddToOrganizationUnitAsync(userId, input.OrganizationUnitId);
            }
        }
        /// <summary>
        /// 点击添加成员-->弹出用户列表(用)
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [AbpAuthorize(PermissionNames.Pages_OrganizationUnits_ManageMembers)]
        public async Task<PagedResultDto<NameValueDto>> FindUsersAsync(FindOrganizationUnitUsersInput input)
        {
            var userIdsInOrganizationUnit = _userOrganizationUnitRepository.GetAll()
                .Where(uou => uou.OrganizationUnitId == input.OrganizationUnitId)
                .Select(uou => uou.UserId);

            var query = UserManager.Users
                .Where(u => !userIdsInOrganizationUnit.Contains(u.Id))
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
                .ThenBy(u => u.Surname)
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
        /// <summary>
        /// 转换组织对象
        /// </summary>
        /// <param name="organizationUnit"></param>
        /// <returns></returns>
        private async Task<OrganizationUnitDto> CreateOrganizationUnitDtoAsync(OrganizationUnit organizationUnit)
        {
            var dto = ObjectMapper.Map<OrganizationUnitDto>(organizationUnit);
            dto.MemberCount=await _userOrganizationUnitRepository.CountAsync(uou=>uou.OrganizationUnitId==organizationUnit.Id);
            return dto;
        }

    }
}
