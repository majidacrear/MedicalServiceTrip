using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using MedicalServiceTrip.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using Service.Patient;
using Service.Storage;
using Core.Configuration;
using Newtonsoft.Json;

namespace MedicalServiceTrip.Controllers
{
    [Route("api/Patient/[action]")]
    public class PatientController : BaseController
    {
        #region Fields
        private readonly IPatientService _patientService;

        private readonly IStorage _storage;

        private readonly MSTConfig _mSTConfig;
        #endregion

        #region Cors

        public PatientController(IPatientService patientService,IStorage storage,MSTConfig mSTConfig)
        {
            this._patientService = patientService;
            _storage = storage;
            _mSTConfig = mSTConfig;
        }

        #endregion

        #region Methods

        [HttpPost]
        [ActionName("AddPatient")]
        public ServiceResponse<Core.Domain.Patient> AddPatient(Core.Domain.Patient patient, IFormFile file)
        {
            var response = new ServiceResponse<Core.Domain.Patient>();
            try
            {                
                patient = _patientService.AddPatient(patient);
                if(patient.Id > 0 && file != null && file.Length > 0)
                {
                    patient.ImagePath = patient.Id + Path.GetExtension(file.FileName);
                    _storage.StoreFile(patient.ImagePath, _mSTConfig.AzureBlobProfile, file.OpenReadStream());                    
                    _patientService.UpdatePatient(patient);                    
                }
                if(!string.IsNullOrEmpty(patient.ImagePath))
                    patient.ImagePath = _mSTConfig.AzureBlobEndPoint + _mSTConfig.AzureBlobProfile + "/" + patient.ImagePath;
                response.Model = patient;
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
        [ActionName("GetAllMyPatients")]
        public ServiceResponse<IEnumerable<Core.Domain.Patient>> GetAllPatientByOrganizationAndUserId([FromBody]JObject jObject )
        {
            int organizationnId = (int)jObject["OrganizationnId"], userId = (int)jObject["UserId"];
            var response = new ServiceResponse<IEnumerable<Core.Domain.Patient>>();
            try
            {
                var list = _patientService.GetAllPatientByOrganizationAndUserId(organizationnId, userId);
                if(list != null)
                {
                    foreach(var patient in list)
                    {
                        patient.ImagePath = _mSTConfig.AzureBlobEndPoint + _mSTConfig.AzureBlobProfile + "/" + patient.ImagePath;
                    }
                }
                response.Model = list;
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
        [ActionName("GetPatientById")]
        public ServiceResponse<Core.Domain.Patient> GetPatientById([FromBody]JObject jObject)
        {
            int patientId = (int)jObject["PatientId"];
            
            var response = new ServiceResponse<Core.Domain.Patient>();
            try
            {

                //https://mstblob.blob.core.windows.net/profile
                var patient = _patientService.GetPatientById(patientId);
                patient.ImagePath = _mSTConfig.AzureBlobEndPoint + _mSTConfig.AzureBlobProfile + "/" + patient.ImagePath;
                response.Model = patient;
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
                response.Message = GetErrorMessageDetail(ex);
            }
            return response;
        }
        #endregion
    }
}