namespace KGJ.Authorization
{
    public static class PermissionNames
    {
        public const string Pages = "Pages";
        public const string Pages_Tenants = "Pages.Tenants";
        public const string Pages_Tenants_Create = "Pages.Tenants.Create";
        public const string Pages_Tenants_Edit = "Pages.Tenants.Edit";
        public const string Pages_Tenants_Delete = "Pages.Tenants.Delete";


        //为相应的方法声明 增删改查权限
        public const string Pages_Users = "Pages.Users";
        public const string Pages_Users_Create = "Pages.Users.Create";
        public const string Pages_Users_Edit = "Pages.Users.Edit";
        public const string Pages_Users_Delete = "Pages.Users.Delete";

        public const string Pages_Roles = "Pages.Roles";
        public const string Pages_Roles_Create = "Pages.Roles.Create";
        public const string Pages_Roles_Edit = "Pages.Roles.Edit";
        public const string Pages_Roles_Delete = "Pages.Roles.Delete";

        public const string Pages_OrganizationUnits = "Pages.OrganizationUnits";
        public const string Pages_OrganizationUnits_ManageOrganizationTree = "Pages.OrganizationUnits.ManageOrganizationTree";
        public const string Pages_OrganizationUnits_ManageMembers = "Pages.OrganizationUnits.ManageMembers";

        public const string Pages_AuditLogs = "Pages.AuditLogs";

        public const string Pages_MailSubscribe = "Pages.MailSubscribe";
        public const string Pages_MailSubscribe_Create = "Pages.MailSubscribe.Create";
        public const string Pages_MailSubscribe_Edit = "Pages.MailSubscribe.Edit";
        public const string Pages_MailSubscribe_Delete = "Pages.MailSubscribe.Delete";

        //仓库库存管理
        public const string Pages_WareHouse = "Pages.WareHouse";
        //仓库维护
        public const string Pages_WareHouseInfo = "Pages.WareHouseInfo";
        public const string Pages_WareHouseInfo_Create = "Pages.WareHouseInfo.Create";
        public const string Pages_WareHouseInfo_Edit = "Pages.WareHouseInfo.Edit";
        public const string Pages_WareHouseInfo_Delete = "Pages.WareHouseInfo.Delete";
    }
}
