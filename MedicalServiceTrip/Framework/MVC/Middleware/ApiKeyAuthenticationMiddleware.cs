using Framework.MVC.Filters;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Service.Authentication;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Encodings.Web;
using System.Threading.Tasks;

namespace Framework.MVC.Middleware
{
    public class ApiKeyAuthenticationMiddleware
    {
        private readonly RequestDelegate _next;

        public ApiKeyAuthenticationMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {

            var request = context.Request;
            if (!request.Path.Value.Contains("/api/User/RegisterUser") && !request.Path.Value.Contains("/api/User/VerifyUser"))
            {
                if (request.Headers.Keys.Contains("ApiKey"))
                {
                    await _next(context);
                }
                else
                {                    

                    var response = context.Response;
                    response.StatusCode = StatusCodes.Status403Forbidden;
                }
            }
            else
            {
                await _next(context);
            }

            

        }
    }
}
