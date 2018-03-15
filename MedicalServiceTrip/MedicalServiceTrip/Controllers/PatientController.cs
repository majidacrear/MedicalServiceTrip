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
using Microsoft.Azure;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Blob;
using Service.Storage;
using Core.Configuration;

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
                response.Model = patient;
                response.Success = true;
            }
            catch(Exception ex)
            {
                response.Success = false;
                response.Message = (ex.InnerException != null) ? ex.InnerException.Message + " " + ex.StackTrace : ex.Message;
            }
           
            
            return response;
        }

        [HttpPost("UploadFiles")]
        public async Task<IActionResult> Post(List<IFormFile> files)
        {
            long size = files.Sum(f => f.Length);

            // full path to file in temp location
            var filePath = Path.GetTempFileName();

            foreach (var formFile in files)
            {
                if (formFile.Length > 0)
                {
                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        await formFile.CopyToAsync(stream);
                    }
                }
            }

            // process uploaded files
            // Don't rely on or trust the FileName property without validation.

            return Ok(new { count = files.Count, size, filePath });
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

                //https://mstblob.blob.core.windows.net/profile
                var patient = _patientService.GetPatientById(patientId);
                patient.ImagePath = _mSTConfig.AzureBlobEndPoint + _mSTConfig.AzureBlobProfile + "/" + patient.ImagePath;
                response.Model = patient;
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