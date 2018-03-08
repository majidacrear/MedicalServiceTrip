using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Core;
using MedicalServiceTrip.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Service.Email;
using Service.Users;

namespace MedicalServiceTrip.Controllers
{
    [Produces("application/json")]
    [Route("api/User/[action]")]
    public class UserController : BaseController
    {
        #region Fields

        private readonly IWebHelper _webHelper;

        private readonly IUserService _userService;

        private readonly IEmailService _emailService;
        #endregion

        #region Cors
        public UserController(IWebHelper webHelper,IUserService userService,IEmailService emailService)
        {
            this._webHelper = webHelper;
            this._userService = userService;
            _emailService = emailService;
        }
        #endregion

        #region Methods

        [HttpPost]
        [ActionName("RegisterUser")]
        public ServiceResponse<Core.Domain.Users> RegisterUser([FromBody]JObject jObject)
        {
            var response = new ServiceResponse<Core.Domain.Users>();
            try
            {
                var user = jObject.ToObject<Core.Domain.Users>();
                String salt = _webHelper.RandomString(_webHelper.RandomStringSize) + "=";
                String password = _webHelper.ComputeHash(user.Password, salt, HashName.MD5);
                user.Password = password;
                user.PasswordSalt = salt;
                var userId = _userService.RegisterUser(user);
                if (userId > 0)
                {
                    _emailService.SendEmail("New Registration", "Dear " + user.FullName + "<br/> Thank you for registering with us. Here is your code <b>" + user.MyCode + "</b>", user.Email, null, null);
                }
                response.Model = user;
                response.Success = true;
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = ex.Message;
            }
            return response;
        }

        [HttpPost]
        [ActionName("ActivateUser")]
        public ServiceResponse<bool> ActivateUser([FromBody]JObject jObject)
        {
            var id = (int)jObject["id"];
            var response = new ServiceResponse<bool>();
            try
            {
                response.Model = _userService.ActivateUser(id);
                response.Success = true;
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = ex.Message;
            }
            return response;
        }

        [HttpPost]
        [ActionName("DeactivateUser")]
        public ServiceResponse<bool> DeactivateUser([FromBody]JObject jObject)
        {
            var id = (int)jObject["id"];
            var response = new ServiceResponse<bool>();
            try
            {
                response.Model = _userService.DeactivateUser(id);
                response.Success = true;
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = ex.Message;
            }
            return response;
        }

        [HttpGet]
        [ActionName("GetUsersByOrganizationId")]
        public ServiceResponse<IEnumerable<Core.Domain.Users>> GetUsersByOrganizationId([FromBody]JObject jObject)
        {
            var organizationId = (int)jObject["organizationId"];
            var response = new ServiceResponse<IEnumerable<Core.Domain.Users>>();
            try
            {
                response.Model = _userService.GetUsersByOrganizationId(organizationId);
                response.Success = true;
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = ex.Message;
            }
            return response;
        }

        [HttpPost]
        [ActionName("VerifyUser")]
        public ServiceResponse<Core.Domain.Users> VerifyUser([FromBody]JObject jObject)
        {
            var username = (string)jObject["username"];
            var password = (string)jObject["password"];
            var deviceNumber = (string)jObject["deviceNumber"];
            var response = new ServiceResponse<Core.Domain.Users>();
            try
            {
                response.Model = _userService.VerifyUser(username, password, deviceNumber);
                response.Success = true;
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = ex.Message;
            }
            return response;
        }
        #endregion

        #region Private Methods

        #endregion
    }
}