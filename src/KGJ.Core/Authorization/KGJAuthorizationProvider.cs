using Abp.Authorization;
using Abp.Localization;
using Abp.MultiTenancy;

namespace KGJ.Authorization
{
    public class KGJAuthorizationProvider : AuthorizationProvider
    {
        public override void SetPermissions(IPermissionDefinitionContext context)
        {
            //context.CreatePermission(PermissionNames.Pages_Users, L("Users"));
            //context.CreatePermission(PermissionNames.Pages_Roles, L("Roles"));
            //context.CreatePermission(PermissionNames.Pages_Tenants, L("Tenants"), multiTenancySides: MultiTenancySides.Host);

            var pages = context.GetPermissionOrNull(PermissionNames.Pages) ?? context.CreatePermission(PermissionNames.Pages, L("Pages"));
            var users = pages.CreateChildPermission(PermissionNames.Pages_Users, L("Users"));
            users.CreateChildPermission(PermissionNames.Pages_Users_Create, L("CreateUser"));
            users.CreateChildPermission(PermissionNames.Pages_Users_Delete, L("DeleteUser"));
            users.CreateChildPermission(PermissionNames.Pages_Users_Edit, L("EditUser"));

            var roles = pages.CreateChildPermission(PermissionNames.Pages_Roles, L("Roles"));
            roles.CreateChildPermission(PermissionNames.Pages_Roles_Create, L("CreateRole"));
            roles.CreateChildPermission(PermissionNames.Pages_Roles_Delete, L("DeleteRole"));
            roles.CreateChildPermission(PermissionNames.Pages_Roles_Edit, L("EditRole"));

            var tenants = pages.CreateChildPermission(PermissionNames.Pages_Tenants, L("Tenants"), multiTenancySides: MultiTenancySides.Host);
            tenants.CreateChildPermission(PermissionNames.Pages_Tenants_Create, L("CreateTenants"), multiTenancySides: MultiTenancySides.Host);
            tenants.CreateChildPermission(PermissionNames.Pages_Tenants_Delete, L("DeleteTenants"), multiTenancySides: MultiTenancySides.Host);
            tenants.CreateChildPermission(PermissionNames.Pages_Tenants_Edit, L("EditTenants"), multiTenancySides: MultiTenancySides.Host);

            var organizationUnits = pages.CreateChildPermission(PermissionNames.Pages_OrganizationUnits, L("OrganizationUnits"));
            organizationUnits.CreateChildPermission(PermissionNames.Pages_OrganizationUnits_ManageOrganizationTree, L("ManagingOrganizationTree"));
            organizationUnits.CreateChildPermission(PermissionNames.Pages_OrganizationUnits_ManageMembers, L("ManagingMembers"));


            ///
            var wareHouse = pages.CreateChildPermission(PermissionNames.Pages_WareHouse, L("WareHouse"));

            var wareHouseInfo = wareHouse.CreateChildPermission(PermissionNames.Pages_WareHouseInfo, L("WareHouseInfo"));
            wareHouseInfo.CreateChildPermission(PermissionNames.Pages_WareHouseInfo_Create, L("CreateWareHouseInfo"));
            wareHouseInfo.CreateChildPermission(PermissionNames.Pages_WareHouseInfo_Delete, L("DeleteWareHouseInfo"));
            wareHouseInfo.CreateChildPermission(PermissionNames.Pages_WareHouseInfo_Edit, L("EditWareHouseInfo"));


            var auditLog = pages.CreateChildPermission(PermissionNames.Pages_AuditLogs, L("AuditLog"));

            var mailSubscribe = pages.CreateChildPermission(PermissionNames.Pages_MailSubscribe, L("MailSubscribe"));
            mailSubscribe.CreateChildPermission(PermissionNames.Pages_MailSubscribe_Create, L("CreateNewMailSubscribe"));
            mailSubscribe.CreateChildPermission(PermissionNames.Pages_MailSubscribe_Edit, L("EditNewMailSubscribe"));
            mailSubscribe.CreateChildPermission(PermissionNames.Pages_MailSubscribe_Delete, L("DeleteMailSubscribe"));
        }

        private static ILocalizableString L(string name)
        {
            return new LocalizableString(name, KGJConsts.LocalizationSourceName);
        }
    }
}
