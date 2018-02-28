using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.Data;
using Core.Domain;

namespace Service.Patient
{
    public class PatientService : IPatientService
    {
        #region Fields

        private readonly IRepository<Core.Domain.Patient> _patientRepository;
        private readonly IRepository<Core.Domain.Users> _userRepository;

        #endregion

        #region Cors

        public PatientService(IRepository<Core.Domain.Patient> patientRepository,IRepository<Core.Domain.Users> userRepository)
        {
            this._patientRepository = patientRepository;
            this._userRepository = userRepository;
        }
        #endregion

        public int AddPatient(Core.Domain.Patient patient)
        {
            var checkPatient = _patientRepository.Table.Where(p => p.PatientIdNumber == patient.PatientIdNumber).FirstOrDefault();
            if (checkPatient == null)
            {
                patient.CreatedDate = DateTime.Now;
                _patientRepository.Insert(patient);
                return patient.Id;
            }
            else
               return checkPatient.Id;
        }
        
        public IEnumerable<Core.Domain.Patient> GetAllPatientByOrganizationAndUserId(int organizationnId,int userId)
        {
            return _patientRepository.Table.Where(p => p.OrganizationId == organizationnId && p.DoctorId == userId && p.IsDeleted == false).ToList();
        }

        public Core.Domain.Patient GetPatientById(int patientId)
        {
            return _patientRepository.GetById(patientId);
        }

        public bool TransferPatient(int patientId, int toDoctorId)
        {
            var patient = _patientRepository.Table.Where(p => p.Id == patientId && p.IsDeleted == false).FirstOrDefault();
            var doctor = _userRepository.Table.Where(d => d.Id == toDoctorId).FirstOrDefault();
            if(patient!= null)
            {                
                patient.DoctorId = toDoctorId;
                _patientRepository.Update(patient);
                return true;
            }
            return false;
        }
    }
}
