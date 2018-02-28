using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using Service.Organization;

namespace MedicalServiceTrip.Controllers
{
    [Produces("application/json")]
    [Route("api/Organization/[action]")]
    public class OrganizationController : Controller
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
        public bool CheckOrganizationExist([FromBody]JObject name)
        {
            return _organizationService.CheckOrganizationExist((string)name["name"]);
        }

        [HttpGet]
        public Core.Domain.Organization Get(int id)
        {
            return _organizationService.GetOrganizationById(id);
        }

        [HttpGet]
        [ActionName("GetAllOrganization")]
        public IEnumerable<Core.Domain.Organization> GetAllOrganization()
        {
            return _organizationService.GetAllOrganization();
        }

        [HttpPost]
        [ActionName("RegisterOrganization")]
        public IEnumerable<Core.Domain.Organization> RegisterOrganization([FromBody]JObject jObject)
        {
            var organization = jObject.ToObject<Core.Domain.Organization>();
            return _organizationService.GetAllOrganization();
        }
        #endregion
    }
}