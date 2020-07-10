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
                new OpenApiTag(){Name = "OrganizationUnit",Description = "组织单位"}
            };
        }
    }
}
