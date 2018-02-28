using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using Service.Users;

namespace MedicalServiceTrip.Controllers
{
    [Produces("application/json")]
    [Route("api/User/[action]")]
    public class UserController : Controller
    {
        #region Fields

        private readonly IWebHelper _webHelper;

        private readonly IUserService _userService;

        #endregion
        #region Cors
        public UserController(IWebHelper webHelper,IUserService userService)
        {
            this._webHelper = webHelper;
            this._userService = userService;
        }
        #endregion

        #region Methods

        [HttpPost]
        public int RegisterUser([FromBody]JObject jObject)
        {
            var user = jObject.ToObject<Core.Domain.Users>();
            String salt = _webHelper.RandomString(_webHelper.RandomStringSize) + "=";
            String password = _webHelper.ComputeHash(user.Password, salt, HashName.MD5);
            user.Password = password;
            user.PasswordSalt = salt;
            return _userService.RegisterUser(user);
        }

        [HttpPost]
        [ActionName("ActivateUser")]
        public bool ActivateUser([FromBody]JObject jObject)
        {
            var id = (int)jObject["id"];
           
            return _userService.ActivateUser(id);
        }

        [HttpPost]
        [ActionName("DeactivateUser")]
        public bool DeactivateUser([FromBody]JObject jObject)
        {
            var id = (int)jObject["id"];

            return _userService.DeactivateUser(id);
        }

        [HttpGet]
        [ActionName("GetUsersByOrganizationId")]
        public IEnumerable<Core.Domain.Users> GetUsersByOrganizationId([FromBody]JObject jObject)
        {
            var organizationId = (int)jObject["organizationId"];

            return _userService.GetUsersByOrganizationId(organizationId);
        }

        [HttpPost]
        [ActionName("VerifyUser")]
        public Core.Domain.Users VerifyUser([FromBody]JObject jObject)
        {
            var username = (string)jObject["username"];
            var password = (string)jObject["password"];

            return _userService.VerifyUser(username,password);
        }
        #endregion
    }
}