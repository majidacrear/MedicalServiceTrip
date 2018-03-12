using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Authentication
{
    public class ApiKeyValidator : IApiKeyValidator
    {
        public Task<bool> ValidateAsync(string apiKey)
        {
            throw new NotImplementedException();
        }
    }
}
