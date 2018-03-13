using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MedicalServiceTrip.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using Service.Patient;

namespace MedicalServiceTrip.Controllers
{
    [Route("api/Patient/[action]")]
    public class PatientController : BaseController
    {
        #region Fields
        private readonly IPatientService _patientService;
        #endregion

        #region Cors

        public PatientController(IPatientService patientService)
        {
            this._patientService = patientService;
        }

        #endregion

        #region Methods

        [HttpPost]
        [ActionName("AddPatient")]
        public ServiceResponse<Core.Domain.Patient> AddPatient([FromBody]JObject jObject, IFormFile file)
        {
            var response = new ServiceResponse<Core.Domain.Patient>();
            try
            {
                var patient = jObject.ToObject<Core.Domain.Patient>();
                response.Model = _patientService.AddPatient(patient);
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
        public async Task<IActionResult> Upload(IFormFile file)
        {
            return null;
        }

        [HttpPost]
        [ActionName("GetAllMyPatients")]
        public ServiceResponse<IEnumerable<Core.Domain.Patient>> GetAllPatientByOrganizationAndUserId([FromBody]JObject jObject )
        {
            int organizationnId = (int)jObject["OrganizationnId"], userId = (int)jObject["UserId"];
            var response = new ServiceResponse<IEnumerable<Core.Domain.Patient>>();
            try
            {
                response.Model = _patientService.GetAllPatientByOrganizationAndUserId(organizationnId, userId);
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
        [ActionName("GetPatientById")]
        public ServiceResponse<Core.Domain.Patient> GetPatientById([FromBody]JObject jObject)
        {
            int patientId = (int)jObject["PatientId"];
            
            var response = new ServiceResponse<Core.Domain.Patient>();
            try
            {
                response.Model = _patientService.GetPatientById(patientId);
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
        [ActionName("TransferPatient")]
        public ServiceResponse<bool> TransferPatient([FromBody]JObject jObject)
        {
            int patientId = (int)jObject["PatientId"], toDoctorId = (int)jObject["ToDoctorId"];
            var response = new ServiceResponse<bool>();
            try
            {
                response.Model = _patientService.TransferPatient(patientId, toDoctorId);
                response.Success = true;
            }
            catch(Exception ex)
            {
                response.Success = false;
                response.Message = ex.Message;
            }
            return response;
        }
        #endregion
    }
}