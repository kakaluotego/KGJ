using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace KGJ.Web.Host.Startup
{
    public class ApplyTagDescriptions : IDocumentFilter
    {
        public void Apply(OpenApiDocument swaggerDoc, DocumentFilterContext context)
        {
            swaggerDoc.Tags = new List<OpenApiTag>
            {
                new OpenApiTag() {Name = "Account", Description = "登录相关接口"},
                new OpenApiTag() {Name = "Common", Description = "公用接口"},
                new OpenApiTag(){Name = "OrganizationUnit",Description = "组织单位"},
                new OpenApiTag(){Name = "AuditLogs",Description = "日志审计"},
                new OpenApiTag(){Name = "CommonLookup",Description = "公用下拉框"},
                new OpenApiTag(){Name = "Configuration",Description = "配置"},
                new OpenApiTag(){Name = "MailSubscribe",Description = "邮件设置"},
                new OpenApiTag(){Name = "Product",Description = "产品"},
                new OpenApiTag(){Name = "ProductCustomField",Description = "产品自定义字段"},
                new OpenApiTag(){Name = "Role",Description = "角色"},
                new OpenApiTag(){Name = "Session",Description = "Session"},
                new OpenApiTag(){Name = "SystemCode",Description = "数据字典"},
                new OpenApiTag(){Name = "Tenant",Description = "租户"},
                new OpenApiTag(){Name = "TokenAuth",Description = "TokenAuth"},
                new OpenApiTag(){Name = "User",Description = "用户"},
                new OpenApiTag(){Name = "WareHouseInfo",Description = "仓库基础"},
                new OpenApiTag(){Name = "WareHouseIOForm",Description = "仓库出入库"}

            };
        }
    }
}
