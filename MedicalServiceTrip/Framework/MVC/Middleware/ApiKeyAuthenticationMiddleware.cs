using Core;
using Framework.MVC.Filters;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Service.Authentication;
using Service.Users;
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
        private readonly IWebHelper _webHelper;
        private readonly IUserService _userService;

        public ApiKeyAuthenticationMiddleware(RequestDelegate next,IWebHelper webHelper,IUserService userService)
        {
            _next = next;
            _webHelper = webHelper;
            _userService = userService;
        }

        public async Task Invoke(HttpContext context)
        {

            var request = context.Request;
            if (!ExcludeApiUrlForAuthentication.ExcludeList.Contains(request.Path.Value))
            {
                if (request.Headers.Keys.Contains("ApiKey") && request.Headers.Keys.Contains("DeviceNumber"))
                {
                    
                    var key = request.Headers["ApiKey"];
                    var deviceNumber = request.Headers["DeviceNumber"];
                    var originalValue = string.Empty;
                    try
                    {
                        originalValue = _webHelper.Decrypt(key);
                    }
                    catch
                    {
                        context.Response.StatusCode = StatusCodes.Status401Unauthorized;
                    }
                    string[] array = originalValue.Split(new string[] { "@_@" },StringSplitOptions.RemoveEmptyEntries);
                    
                    if (array.Count() == 3 && int.TryParse(array[0], out int userId))
                    {
                        try
                        {
                            var user = _userService.GetUserById(userId);
                            if (user.IsActive && !user.IsDeleted)
                            {
                                if (user.DeviceNumber.Equals(array[1]) && user.DeviceNumber.Equals(deviceNumber))
                                {
                                    await _next(context);
                                }
                                else
                                {
                                    context.Response.StatusCode = StatusCodes.Status401Unauthorized;
                                }
                            }
                            else
                            { context.Response.StatusCode = StatusCodes.Status401Unauthorized; }
                        }
                        catch(Exception ex)
                        {
                            context.Response.StatusCode = StatusCodes.Status401Unauthorized;
                        }
                    }
                }
                else
                {                    

                    var response = context.Response;
                    response.StatusCode = StatusCodes.Status400BadRequest;
                }
            }
            else
            {
                await _next(context);
            }

            

        }
    }

    internal class ExcludeApiUrlForAuthentication
    {
        public static string[] ExcludeList = {  "/api/",
                                                "/api/User/RegisterUser" ,
                                                "/api/User/VerifyUser",
                                                "/api/Organization/CheckOrganizationExist",
                                                 "/api/Organization/GetAllOrganization",
                                                "/api/Country/GetCountryList"};
    }
}
