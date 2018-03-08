using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using MedicalServiceTrip.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using Service.Organization;

namespace MedicalServiceTrip.Controllers
{
    [Produces("application/json")]
    [Route("api/Organization/[action]")]
    public class OrganizationController : BaseController
    {
        #region Fields

        private readonly IOrganizationService _organizationService;
        #endregion

        #region Cors
                
        public OrganizationController(IOrganizationService organizationService)
        {
            _organizationService = organizationService;
        }

        #endregion

        #region Methods

        [HttpPost]
        [ActionName("CheckOrganizationExist")]
        public ServiceResponse<bool> CheckOrganizationExist([FromBody]JObject name)
        {
            var response = new ServiceResponse<bool>();
            try
            {
                response.Model = _organizationService.CheckOrganizationExist((string)name["name"]);
                response.Success = true;
            }
            catch(Exception ex)
            {
                response.Success = false;
                response.Message = ex.Message;

            }
            return response;
        }

        [HttpGet]
        public ServiceResponse<Core.Domain.Organization> Get(int id)
        {
            var response = new ServiceResponse<Core.Domain.Organization>();
            try
            {
                response.Model = _organizationService.GetOrganizationById(id);
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
        [ActionName("GetAllOrganization")]
        public ServiceResponse<IEnumerable<Core.Domain.Organization>> GetAllOrganization()
        {
            var response = new ServiceResponse<IEnumerable<Core.Domain.Organization>>();
            try
            {
                response.Model= _organizationService.GetAllOrganization();
                response.Success = true;
            }
            catch(Exception ex)
            {
                response.Success = false;
                response.Message = ex.Message;
            }
            return response;
        }

        [HttpPost]
        [ActionName("RegisterOrganization")]
        public ServiceResponse<Core.Domain.Organization> RegisterOrganization([FromBody]JObject jObject)
        {
            var response = new ServiceResponse<Core.Domain.Organization>();
            try
            {
                var organization = jObject.ToObject<Core.Domain.Organization>();
                var id = _organizationService.RegisterOrganization(organization);
                if(id>0)
                {
                    response.Model = organization;
                    response.Success = true;
                }
                else
                {
                    response.Success = false;
                }
            }
            catch(Exception ex)
            {
                response.Message = ex.Message;
                response.Success = false;
            }
            return response;
        }
        #endregion
    }
}