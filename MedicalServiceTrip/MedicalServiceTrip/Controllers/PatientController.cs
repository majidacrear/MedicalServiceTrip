using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using Service.Patient;

namespace MedicalServiceTrip.Controllers
{
    [Produces("application/json")]
    [Route("api/Patient/[action]")]
    public class PatientController : Controller
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
        public int AddPatient(JObject jObject)
        {
            var patient = jObject.ToObject<Core.Domain.Patient>();
            return _patientService.AddPatient(patient);
        }

        [HttpPost]
        [ActionName("GetAllMyPatients")]
        public IEnumerable<Core.Domain.Patient> GetAllPatientByOrganizationAndUserId(int organizationnId, int userId)
        {
            return _patientService.GetAllPatientByOrganizationAndUserId(organizationnId,userId);
        }

        [HttpPost]
        [ActionName("GetPatientById")]
        public Core.Domain.Patient GetPatientById(int patientId)
        {
            return _patientService.GetPatientById(patientId);
        }

        [HttpPost]
        [ActionName("TransferPatient")]
        public bool TransferPatient(int patientId, int toDoctorId)
        {
            return _patientService.TransferPatient(patientId,toDoctorId);
        }
        #endregion
    }
}