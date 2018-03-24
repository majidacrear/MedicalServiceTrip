using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.Data;
using Core.Domain;
using System.Data.Entity;

namespace Service.Patient
{
    public class PatientService : IPatientService
    {
        #region Fields

        private readonly IRepository<Core.Domain.Patient> _patientRepository;
        private readonly IRepository<Core.Domain.Users> _userRepository;
        private readonly IRepository<PatientVisit> _patientVisitRepository;
        private readonly IRepository<VitalSigns> _vitalSignsRepository;

        #endregion

        #region Cors

        public PatientService(IRepository<Core.Domain.Patient> patientRepository,IRepository<Core.Domain.Users> userRepository
                                    , IRepository<PatientVisit> patientVisitRepository
                                    , IRepository<VitalSigns> vitalSignsRepository)
        {
            this._patientRepository = patientRepository;
            this._userRepository = userRepository;
            _patientVisitRepository = patientVisitRepository;
            _vitalSignsRepository = vitalSignsRepository;
        }
        #endregion

        public Core.Domain.Patient AddPatient(Core.Domain.Patient patient)
        {
            if (patient == null)
                throw new ArgumentNullException(nameof(patient));            
            var checkPatient = _patientRepository.Table.Where(p => p.PatientIdNumber == patient.PatientIdNumber && p.OrganizationId == patient.OrganizationId).FirstOrDefault();
            if (checkPatient == null)
            {
                patient.CreatedDate = DateTime.Now;
                _patientRepository.Insert(patient);
                return patient;
            }
            else
            {
                //checkPatient.Age = patient.Age;
                //checkPatient.FullName = patient.FullName;
                //checkPatient.Location = patient.Location;
                //checkPatient.PhoneNumber = patient.PhoneNumber;
                return checkPatient;
            }
        }
        
        public IEnumerable<Core.Domain.Patient> GetAllPatientByOrganizationAndUserId(int organizationnId,int userId)
        {
            var patientList = _patientRepository.TableNoTracking.Where(p => p.OrganizationId == organizationnId && p.DoctorId == userId && p.IsDeleted == false).ToList();
            foreach(var patient in patientList)
            {
                var patientVisit = _patientVisitRepository.Table.Where(pv => pv.PatientId == patient.Id).Include(pv=>pv.VitalSigns).ToList();
                if(patientVisit != null && patientVisit.Where(pv=>pv.VisitCompleted == false).Count() > 0)
                {
                    var pvs = patientVisit.Where(pv => pv.VisitCompleted == false).FirstOrDefault();
                    var vitalSigns = _vitalSignsRepository.Table.Where(vs=>vs.PatientVisitId == pvs.Id).FirstOrDefault();
                    if(vitalSigns == null)
                    {
                        patient.PatientVisitStatus = "Waiting";
                    }
                    else if(vitalSigns != null)
                    {
                        patient.PatientVisitStatus = "Active";
                    }                    
                }
                else if(patientVisit != null && patientVisit.Count() > 0)
                {
                    patient.PatientVisitStatus = "Completed";
                }
            }

            return patientList;
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

        public void UpdatePatient(Core.Domain.Patient patient)
        {
            _patientRepository.Update(patient);
        }
    }
}
