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
                    user.Id = userId;
                    _emailService.SendEmail("New Registration", "Welcome to Q2 " + user.FullName + ".<br/> This email is sent to confirm your registration with Q2. You can invite your friends and colleagues by sharing this code  <b>" + user.MyCode + "</b>. This code must be inserted in the designated field in the registration form. <br/>Again, thank you for joining Q2!<br/> Cheers! <br/>Q2", user.Email, null, null);
                }
                response.Model = user;
                response.Success = true;
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = GetErrorMessageDetail(ex);
            }
            return response;
        }

        [HttpPost]
        [ActionName("ActivateUser")]
        public ServiceResponse<bool> ActivateUser([FromBody]JObject jObject)
        {
            var id = (int)jObject["Id"];
            var response = new ServiceResponse<bool>();
            try
            {
                response.Model = _userService.ActivateUser(id);
                response.Success = true;
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = GetErrorMessageDetail(ex);
            }
            return response;
        }

        [HttpPost]
        [ActionName("DeactivateUser")]
        public ServiceResponse<bool> DeactivateUser([FromBody]JObject jObject)
        {
            var id = (int)jObject["Id"];
            var response = new ServiceResponse<bool>();
            try
            {
                response.Model = _userService.DeactivateUser(id);
                response.Success = true;
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = GetErrorMessageDetail(ex);
            }
            return response;
        }

        [HttpPost]
        [ActionName("GetUsersByOrganizationId")]
        public ServiceResponse<IEnumerable<Core.Domain.Users>> GetUsersByOrganizationId([FromBody]JObject jObject)
        {
            var organizationId = (int)jObject["OrganizationId"];
            var response = new ServiceResponse<IEnumerable<Core.Domain.Users>>();
            try
            {
                response.Model = _userService.GetUsersByOrganizationId(organizationId);
                response.Success = true;
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = GetErrorMessageDetail(ex);
            }
            return response;
        }

        [HttpPost]
        [ActionName("VerifyUser")]
        public ServiceResponse<Core.Domain.Users> VerifyUser([FromBody]JObject jObject)
        {
            var username = (string)jObject["Username"];
            var password = (string)jObject["Password"];
            var deviceNumber = (string)jObject["DeviceNumber"];
            var response = new ServiceResponse<Core.Domain.Users>();
            try
            {
                var user = _userService.VerifyUser(username, password, deviceNumber);
                if (user != null)
                {
                    var key = _webHelper.Encrypt(user.Id + "@_@" + user.DeviceNumber + "@_@" + DateTime.Now.ToString());
                    this.Response.Headers.Add("ApiKey", key);
                    user.ApiKey = key;
                    response.Model = user;
                    response.Success = true;
                }
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = GetErrorMessageDetail(ex);
            }
            return response;
        }

        [HttpPost]
        [ActionName("ChangeUserPinCode")]
        public ServiceResponse<bool> ChangeUserPinCode([FromBody]JObject jObject)
        {
            var userId = (int)jObject["UserId"];
            var pinCode = (int)jObject["PinCode"];
            var password = (string)jObject["Password"];
            var response = new ServiceResponse<bool>();
            try
            {               
                _userService.ChangeUserPin(userId, pinCode,password);
                response.Model = true;
                response.Success = true;
            }
            catch(Exception ex)
            {
                response.Success = false;
                response.Message = GetErrorMessageDetail(ex);
            }
            return response;
        }

        [HttpPost]
        [ActionName("GetNonOrganizationUsers")]
        public ServiceResponse<IEnumerable<Core.Domain.Users>> GetNonOrganizationUsers()
        {
            var response = new ServiceResponse<IEnumerable<Core.Domain.Users>>();
            try
            {
                response.Model = _userService.GetNonOrganizationUsers();
                response.Success = true;
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = GetErrorMessageDetail(ex);
            }
            return response;
        }
        #endregion

        #region Private Methods

        #endregion
    }
}