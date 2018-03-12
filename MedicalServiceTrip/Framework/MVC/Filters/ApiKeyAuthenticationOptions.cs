using Microsoft.AspNetCore.Authentication;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Framework.MVC.Filters
{
    public class ApiKeyAuthenticationOptions : AuthenticationOptions
    {
        public const string DefaultHeaderName = "Authorization";
        public string HeaderName { get; set; } = DefaultHeaderName;
    }
}
