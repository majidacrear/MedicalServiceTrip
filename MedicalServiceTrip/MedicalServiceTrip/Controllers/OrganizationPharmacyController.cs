using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MedicalServiceTrip.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using Service.OrganizationPharmacy;

namespace MedicalServiceTrip.Controllers
{
    [Produces("application/json")]
    [Route("api/OrganizationPharmacy/[action]")]
    public class OrganizationPharmacyController : BaseController
    {
        #region Fields

        private readonly IOrganizationPharmacyService _organizationPharmacyService;

        #endregion

        #region Cors

        public OrganizationPharmacyController(IOrganizationPharmacyService organizationPharmacyService)
        {
            _organizationPharmacyService = organizationPharmacyService;
        }

        #endregion

        #region Methods

        [HttpPost]
        [ActionName("GetOrganizationPharmacy")]
        public ServiceResponse<IEnumerable<Core.Domain.OrganizationPharmacy>> GetOrganizationPharmacy([FromBody]JObject jObject)
        {
            var response = new ServiceResponse<IEnumerable<Core.Domain.OrganizationPharmacy>>();
            try
            {
                var organizationId = (int)jObject["OrganizationId"];                
                response.Model = _organizationPharmacyService.GetOrganizationPharmacy(organizationId);
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
        [ActionName("AddOrganizationPharmacy")]
        public ServiceResponse<Core.Domain.OrganizationPharmacy> AddOrganizationPharmacy([FromBody]JObject jObject)
        {
            var response = new ServiceResponse<Core.Domain.OrganizationPharmacy>();
            try
            {
                var organizationPharmacy = jObject.ToObject<Core.Domain.OrganizationPharmacy>();
                organizationPharmacy = _organizationPharmacyService.AddOrganizationPharmacy(organizationPharmacy);

                response.Model = organizationPharmacy;
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

    }
}