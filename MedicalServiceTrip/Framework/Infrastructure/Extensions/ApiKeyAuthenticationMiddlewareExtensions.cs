using Framework.MVC.Middleware;
using Microsoft.AspNetCore.Builder;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Framework.Infrastructure.Extensions
{
    public static class ApiKeyAuthenticationMiddlewareExtensions
    {
        public static IApplicationBuilder UseApiKeyAuthenticationMiddleware(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<ApiKeyAuthenticationMiddleware>();
        }
    }
}
