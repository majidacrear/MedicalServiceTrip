using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Domain;
using MedicalServiceTrip.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using Service.Patient;

namespace MedicalServiceTrip.Controllers
{
    [Produces("application/json")]
    [Route("api/PatientVisit/[action]")]
    public class PatientVisitController : BaseController
    {
        #region Fields

        private readonly IPatientVisitService _patientVisitService;
        #endregion

        #region Cors

        public PatientVisitController(IPatientVisitService patientVisitService)
        {
            _patientVisitService = patientVisitService;
        }
        #endregion

        #region Mehtods

        [HttpPost]
        [ActionName("StartPatientVisit")]
        public ServiceResponse<Core.Domain.PatientVisit> StartPatientVisit([FromBody]JObject jObject)
        {
            var response = new ServiceResponse<Core.Domain.PatientVisit>();
            try
            {
                var patientVisit = jObject.ToObject<Core.Domain.PatientVisit>();
                response.Model = _patientVisitService.AddPatientVisit(patientVisit);
                response.Success = true;
            }
            catch(Exception ex)
            {
                response.Success = false;
                response.Message = GetErrorMessageDetail(ex);
            }
            return response;
        }
        #endregion
    }
}