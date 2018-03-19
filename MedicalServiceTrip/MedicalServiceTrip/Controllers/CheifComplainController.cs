using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MedicalServiceTrip.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using Service.CheifComplain;

namespace MedicalServiceTrip.Controllers
{
    [Produces("application/json")]
    [Route("api/CheifComplain/[action]")]
    public class CheifComplainController : BaseController
    {
        #region Fields

        private readonly ICheifComplainService _cheifComplainService;

        #endregion

        #region Cors

        public CheifComplainController(ICheifComplainService cheifComplainService)
        {
            _cheifComplainService = cheifComplainService;
        }

        #endregion

        #region Methods

        [HttpPost]
        [ActionName("AddCheifComplain")]
        public ServiceResponse<Core.Domain.CheifComplain>  AddCheifComplain([FromBody]JObject jObject)
        {
            var response = new ServiceResponse<Core.Domain.CheifComplain>();
            try
            {
                var cheifComplain = jObject.ToObject<Core.Domain.CheifComplain>();
                
                response.Model = _cheifComplainService.AddCheifComplain(cheifComplain);
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
        [ActionName("GetCheifComplainList")]
        public ServiceResponse<IEnumerable< Core.Domain.CheifComplain>> GetCheifComplainList([FromBody]JObject jObject)
        {
            var response = new ServiceResponse<IEnumerable<Core.Domain.CheifComplain>>();
            try
            {
                var organizationId = (int)jObject["OrganizationId"];

                response.Model = _cheifComplainService.GetCheifComplainList(organizationId);
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