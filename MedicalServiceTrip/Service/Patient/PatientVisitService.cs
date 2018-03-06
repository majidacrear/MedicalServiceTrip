using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.Data;
using Core.Domain;

namespace Service.Patient
{
    public class PatientVisitService : IPatientVisitService
    {

        #region Fields
        private readonly IRepository<PatientVisit> _patientVisitRepository;

        private readonly IRepository<VitalSigns> _vitalSignsRepository;

        private readonly IRepository<PatientVisitCheifComplain> _cheifComplainRepository;
        #endregion

        #region Cors

        public PatientVisitService(IRepository<PatientVisit> patientVisitRepository, IRepository<VitalSigns> vitalSignsRepository
                            , IRepository<PatientVisitCheifComplain> cheifComplainRepository)
        {
            _patientVisitRepository = patientVisitRepository;
            _vitalSignsRepository = vitalSignsRepository;
            _cheifComplainRepository = cheifComplainRepository;
        }
        #endregion

        #region Methods


        public PatientVisit AddPatientVisit(PatientVisit patientVisit)
        {
            if (patientVisit == null)
                throw new ArgumentNullException(nameof(patientVisit));
            if(patientVisit.Id<=0)
            {
                patientVisit.CreatedDate = DateTime.Now;
                _patientVisitRepository.Insert(patientVisit);
            }
            if(patientVisit.VitalSigns != null) /// Add Or Update Vital Signs during patient visit.
            {
                if (patientVisit.VitalSigns.Id <= 0)
                {
                    _vitalSignsRepository.Insert(patientVisit.VitalSigns);
                }
                else
                {
                    _vitalSignsRepository.Update(patientVisit.VitalSigns);
                }
            }

            if(patientVisit.PatientVisitCheifComplain != null)
            {
                
                _cheifComplainRepository.Insert(patientVisit.PatientVisitCheifComplain);
            }
            return patientVisit;
        }

        public PatientVisit GetPatientVisitByPatientId(int patientId)
        {
            return _patientVisitRepository.Table.Where(pv => pv.PatientId == patientId && pv.VisitCompleted == false).FirstOrDefault();
        }

        public IEnumerable<PatientVisit> GetPatientVisitHistory(int patientId)
        {
            return _patientVisitRepository.Table.Where(pv => pv.PatientId == patientId && pv.VisitCompleted == true).ToList();
        }
        #endregion
    }
}
