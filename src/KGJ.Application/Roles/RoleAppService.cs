using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using Abp.Authorization;
using Abp.Domain.Repositories;
using Abp.Extensions;
using Abp.IdentityFramework;
using Abp.Linq.Extensions;
using DBRS.Roles.Dto;
using KGJ.Authorization;
using KGJ.Authorization.Roles;
using KGJ.Authorization.Users;
using KGJ.Roles.Dto;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace KGJ.Roles
{
    [AbpAuthorize(PermissionNames.Pages_Roles)]
    public class RoleAppService : AsyncCrudAppService<Role, RoleDto, int, PagedRoleResultRequestDto, CreateRoleDto, RoleDto>, IRoleAppService
    {
        private readonly RoleManager _roleManager;
        private readonly UserManager _userManager;

        public RoleAppService(IRepository<Role> repository, RoleManager roleManager, UserManager userManager)
            : base(repository)
        {
            _roleManager = roleManager;
            _userManager = userManager;
        }

        public override async Task<RoleDto> CreateAsync(CreateRoleDto input)
        {
            CheckCreatePermission();

            var role = ObjectMapper.Map<Role>(input);
            role.SetNormalizedName();

            CheckErrors(await _roleManager.CreateAsync(role));

            var grantedPermissions = PermissionManager
                .GetAllPermissions()
                .Where(p => input.GrantedPermissions.Contains(p.Name))
                .ToList();

            await _roleManager.SetGrantedPermissionsAsync(role, grantedPermissions);

            return MapToEntityDto(role);
        }

        public async Task<ListResultDto<RoleListDto>> GetRolesAsync(GetRolesInput input)
        {
            var roles = await _roleManager
                .Roles
                .WhereIf(
                    !input.Permission.IsNullOrWhiteSpace(),
                    r => r.Permissions.Any(rp => rp.Name == input.Permission && rp.IsGranted)
                )
                .ToListAsync();

            return new ListResultDto<RoleListDto>(ObjectMapper.Map<List<RoleListDto>>(roles));
        }

        public override async Task<RoleDto> UpdateAsync(RoleDto input)
        {
            CheckUpdatePermission();

            var role = await _roleManager.GetRoleByIdAsync(input.Id);

            ObjectMapper.Map(input, role);

            CheckErrors(await _roleManager.UpdateAsync(role));

            var grantedPermissions = PermissionManager
                .GetAllPermissions()
                .Where(p => input.GrantedPermissions.Contains(p.Name))
                .ToList();

            await _roleManager.SetGrantedPermissionsAsync(role, grantedPermissions);

            return MapToEntityDto(role);
        }

        public override async Task DeleteAsync(EntityDto<int> input)
        {
            CheckDeletePermission();

            var role = await _roleManager.FindByIdAsync(input.Id.ToString());
            var users = await _userManager.GetUsersInRoleAsync(role.NormalizedName);

            foreach (var user in users)
            {
                CheckErrors(await _userManager.RemoveFromRoleAsync(user, role.NormalizedName));
            }

            CheckErrors(await _roleManager.DeleteAsync(role));
        }

        //获得checked全为false,方便create用
        public Task<PermissionTreeDto> GetAllPermissionsTreePage()
        {
            var permissions = PermissionManager.GetAllPermissions(true);
            var permissionTree = new PermissionTreeDto();
            foreach (var permission in permissions)
            {
                if (permission.Parent == null)
                {
                    permissionTree = ObjectMapper.Map<PermissionTreeDto>(permission);
                    int level = 0;
                    GetAllPermissionToTree(permissionTree, level);
                }
            }
            return Task.FromResult(ObjectMapper.Map<PermissionTreeDto>(permissionTree));

        }

        private void GetAllPermissionToTree(PermissionTreeDto permissionTreeDto, int level)
        {
            permissionTreeDto.Title = permissionTreeDto.DisplayName;
            level = level + 1;
            permissionTreeDto.Level = level;
            if (level < 3)
            {
                permissionTreeDto.Expand = true;
            }
            if (permissionTreeDto.Children.Count == 0)
            {
                return;
            }
            for (int i = permissionTreeDto.Children.Count - 1; i >= 0; i--)
            {
                if (AbpSession.TenantId.HasValue)       //当前登录的是租户，就把MultiTenancySides 是host的剔除
                {
                    if (permissionTreeDto.Children[i].MultiTenancySides == Abp.MultiTenancy.MultiTenancySides.Host)
                    {
                        permissionTreeDto.Children.Remove(permissionTreeDto.Children[i]);
                        continue;
                    }
                }

                GetAllPermissionToTree(permissionTreeDto.Children[i], level);
            }
            return;
        }

        //获得角色权限，edit用
        public async Task<ListResultDto<PermissionTreeDto>> GetGrantedPermissionsTreePage(EntityDto<int> input)
        {
            var permissions = PermissionManager.GetAllPermissions();
            var role = await _roleManager.GetRoleByIdAsync(input.Id);
            var grantedPermissions = (await _roleManager.GetGrantedPermissionsAsync(role)).ToArray();
            var grantedPermissionNames = grantedPermissions.Select(p => p.Name).ToList();
            var permissionTree = new PermissionTreeDto();
            foreach (var permission in permissions)
            {
                if (permission.Parent == null)
                {
                    permissionTree = ObjectMapper.Map<PermissionTreeDto>(permission);
                    int level = 0;
                    GetGrantedPermissionToTree(permissionTree, grantedPermissionNames, level);
                }
            }
            List<PermissionTreeDto> listTree = new List<PermissionTreeDto>();
            listTree.Add(permissionTree);
            return new ListResultDto<PermissionTreeDto>(listTree);

        }

        private void GetGrantedPermissionToTree(PermissionTreeDto permissionTree, List<string> grantedPermissionNames, int level)
        {
            permissionTree.Title = permissionTree.DisplayName;
            level = level + 1;
            if (level < 3)
            {
                permissionTree.Expand = true;
            }
            permissionTree.Level = level;
            //permissionTreeDto.Expand = true;
            if (grantedPermissionNames.Contains(permissionTree.Name))
            {
                permissionTree.Checked = true;
            }
            if (permissionTree.Children.Count == 0)
            {
                return;
            }
            for (int i = permissionTree.Children.Count - 1; i >= 0; i--)
            {
                if (AbpSession.TenantId.HasValue)
                {
                    if (permissionTree.Children[i].MultiTenancySides == Abp.MultiTenancy.MultiTenancySides.Host)
                    {
                        permissionTree.Children.Remove(permissionTree.Children[i]);
                        continue;
                    }
                }

                GetGrantedPermissionToTree(permissionTree.Children[i], grantedPermissionNames, level);
            }
            return;
        }

        protected override IQueryable<Role> CreateFilteredQuery(PagedRoleResultRequestDto input)
        {
            return Repository.GetAllIncluding(x => x.Permissions)
                .WhereIf(!input.Keyword.IsNullOrWhiteSpace(), x => x.Name.Contains(input.Keyword)
                || x.DisplayName.Contains(input.Keyword)
                || x.Description.Contains(input.Keyword));
        }

        protected override async Task<Role> GetEntityByIdAsync(int id)
        {
            return await Repository.GetAllIncluding(x => x.Permissions).FirstOrDefaultAsync(x => x.Id == id);
        }

        protected override IQueryable<Role> ApplySorting(IQueryable<Role> query, PagedRoleResultRequestDto input)
        {
            return query.OrderBy(r => r.DisplayName);
        }

        protected virtual void CheckErrors(IdentityResult identityResult)
        {
            identityResult.CheckErrors(LocalizationManager);
        }

        public async Task<GetRoleForEditOutput> GetRoleForEdit(EntityDto input)
        {
            var permissions = PermissionManager.GetAllPermissions();
            var role = await _roleManager.GetRoleByIdAsync(input.Id);
            var grantedPermissions = (await _roleManager.GetGrantedPermissionsAsync(role)).ToArray();
            var roleEditDto = ObjectMapper.Map<RoleEditDto>(role);

            return new GetRoleForEditOutput
            {
                Role = roleEditDto,
                Permissions = ObjectMapper.Map<List<FlatPermissionDto>>(permissions).OrderBy(p => p.DisplayName).ToList(),
                GrantedPermissionNames = grantedPermissions.Select(p => p.Name).ToList()
            };
        }
    }
}

