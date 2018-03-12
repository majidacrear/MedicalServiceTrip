using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Authentication
{
    public interface IApiKeyValidator
    {
        Task<bool> ValidateAsync(string apiKey);
    }
}
