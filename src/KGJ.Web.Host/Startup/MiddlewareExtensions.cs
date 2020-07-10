using Microsoft.AspNetCore.Builder;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KGJ.Web.Host.Startup
{
    public static class MiddlewareExtensions
    {
        public static IApplicationBuilder UseHttpContextMiddleware(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<HttpContextMiddleware>();
        }
    }
}
