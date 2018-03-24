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
    public class PatientVisitService : IPatientVisitService
    {

        #region Fields
        private readonly IRepository<PatientVisit> _patientVisitRepository;

        private readonly IRepository<VitalSigns> _vitalSignsRepository;

        private readonly IRepository<PatientVisitCheifComplain> _cheifComplainRepository;

        private readonly IRepository<PatientVisitDiagnosis> _pateintVisitDiagnosis;

        private readonly IRepository<PatientVisitPrescription> _pateintVisitPrescription;
        #endregion

        #region Cors

        public PatientVisitService(IRepository<PatientVisit> patientVisitRepository, IRepository<VitalSigns> vitalSignsRepository
                            , IRepository<PatientVisitCheifComplain> cheifComplainRepository,IRepository<PatientVisitDiagnosis> pateintVisitDiagnosis
                            , IRepository<PatientVisitPrescription> pateintVisitPrescription)
        {
            _patientVisitRepository = patientVisitRepository;
            _vitalSignsRepository = vitalSignsRepository;
            _cheifComplainRepository = cheifComplainRepository;
            _pateintVisitDiagnosis = pateintVisitDiagnosis;
            _pateintVisitPrescription = pateintVisitPrescription;
        }
        #endregion

        #region Methods


        public PatientVisit AddPatientVisit(PatientVisit patientVisit)
        {
            if (patientVisit == null)
                throw new ArgumentNullException(nameof(patientVisit));
            if (patientVisit.Id <= 0 && _patientVisitRepository.Table.Where(pv => !pv.VisitCompleted && pv.PatientId == patientVisit.PatientId).Count() == 0)
            {
                var pv = new PatientVisit() {  //This object is created to avoid related entities saving issue. If original object(patientVisit) is saved with related entities then it throws error.
                    PatientId = patientVisit.PatientId,
                    VisitCompleted = patientVisit.VisitCompleted,
                    PatientHistory = patientVisit.PatientHistory,
                    CreatedDate = DateTime.Now
                };
                _patientVisitRepository.Insert(pv);
                patientVisit.Id = pv.Id;
            }
            else
            {
                var pv = _patientVisitRepository.GetById(patientVisit.Id);
                if (pv == null)
                    pv = _patientVisitRepository.Table.Where(pv1 => !pv1.VisitCompleted && pv1.PatientId == patientVisit.PatientId).FirstOrDefault();
                if (pv != null)
                {
                    pv.VisitCompleted = patientVisit.VisitCompleted;
                    pv.PatientHistory = patientVisit.PatientHistory;
                    pv.UpdatedDate = DateTime.Now;
                    _patientVisitRepository.Update(pv);
                }
            }

            SaveVitalSigns(patientVisit);

            if(patientVisit.PatientVisitCheifComplain != null)
            {
                foreach(var pvcc in patientVisit.PatientVisitCheifComplain)
                {
                    var patientVisitCC = _cheifComplainRepository.Table.Where(pcc => pcc.CheifComplainId == pvcc.CheifComplainId && pcc.PatientVisitId == patientVisit.Id).FirstOrDefault();
                    if (patientVisitCC == null)
                        pvcc.PatientVisitId = patientVisit.Id;
                    else
                        pvcc.Id = patientVisitCC.Id;
                    if (pvcc.Id <=0)
                    {                        
                        pvcc.CreatedDate = DateTime.Now;
                        _cheifComplainRepository.Insert(pvcc);
                    }
                }
                var ccIdArray = patientVisit.PatientVisitCheifComplain.Select(pvcc => pvcc.CheifComplainId).ToArray();
                var deletedList = _cheifComplainRepository.Table.Where(pvcc => !ccIdArray.Contains(pvcc.CheifComplainId) && pvcc.PatientVisitId == patientVisit.Id).ToArray();
                if (deletedList != null)
                {
                    _cheifComplainRepository.Delete(deletedList);
                }
            }
            else
            {
                _cheifComplainRepository.Delete(_cheifComplainRepository.Table.Where(pvcc => pvcc.PatientVisitId == patientVisit.Id).ToArray());
            }

            if(patientVisit.PatientVisitDiagnosis != null)
            {
                foreach(var pvd in patientVisit.PatientVisitDiagnosis)
                {
                    if(pvd.Id <= 0)
                    {
                        pvd.PatientVisitId = patientVisit.Id;
                        if (_pateintVisitDiagnosis.TableNoTracking.Where(pvd1 => pvd1.PatientVisitId == pvd.PatientVisitId && pvd1.DiagnosisId == pvd.DiagnosisId).Count() == 0)
                            _pateintVisitDiagnosis.Insert(pvd);
                    }
                    else
                    {
                        _pateintVisitDiagnosis.Update(pvd);
                    }
                }

                // Delete those entries which were previously added and remove this time.
                var diagnosisIdArray = patientVisit.PatientVisitDiagnosis.Select(pvd => pvd.DiagnosisId).ToArray();
                var deletedList = _pateintVisitDiagnosis.Table.Where(pvd => !diagnosisIdArray.Contains(pvd.DiagnosisId) && pvd.PatientVisitId == patientVisit.Id).ToArray();
                if(deletedList!= null)
                {
                    _pateintVisitDiagnosis.Delete(deletedList);
                }
            }

            if(patientVisit.PatientVisitPrescription != null)
            {
                foreach(var pvp in patientVisit.PatientVisitPrescription)
                {
                    if(pvp.Id <=0)
                    {
                        pvp.PatientVisitId = patientVisit.Id;
                        _pateintVisitPrescription.Insert(pvp);
                    }
                    else
                    {
                        _pateintVisitPrescription.Update(pvp);
                    }
                }

                var prescriptionIdArray = patientVisit.PatientVisitPrescription.Select(pvp => pvp.OrganizationPharmacyId).ToArray();
                var deletedList = _pateintVisitPrescription.Table.Where(pvp => !prescriptionIdArray.Contains(pvp.OrganizationPharmacyId) && pvp.PatientVisitId == patientVisit.Id).ToArray();
                if (deletedList != null)
                {
                    _pateintVisitPrescription.Delete(deletedList);
                }
            }

            patientVisit = _patientVisitRepository.Table.Include(pv=>pv.VitalSigns).Where(pv => pv.Id == patientVisit.Id).FirstOrDefault();
            return patientVisit;
        }

        public PatientVisit GetPatientVisitByPatientId(int patientId)
        {
            return _patientVisitRepository.Table.Where(pv => pv.PatientId == patientId && pv.VisitCompleted == false && pv.IsDeleted == false).FirstOrDefault();
        }

        public IEnumerable<PatientVisit> GetPatientVisitHistory(int patientId)
        {
            return _patientVisitRepository.Table.Where(pv => pv.PatientId == patientId && pv.VisitCompleted == true && pv.IsDeleted == false).ToList();
        }

        #region Protected

        protected void SaveVitalSigns(PatientVisit patientVisit)
        {
            if (patientVisit.VitalSigns != null) /// Add Or Update Vital Signs during patient visit.
            {
                var vitalSigns = _vitalSignsRepository.Table.Where(vs => vs.PatientVisitId == patientVisit.Id).FirstOrDefault();
                if (vitalSigns != null)
                {
                    patientVisit.VitalSigns.ElementAtOrDefault(0).Id = vitalSigns.Id;
                }
                patientVisit.VitalSigns.ElementAtOrDefault(0).PatientVisitId = patientVisit.Id;
                if (patientVisit.VitalSigns.ElementAtOrDefault(0).Id <= 0)
                {
                    patientVisit.VitalSigns.ElementAtOrDefault(0).CreatedDate = DateTime.Now;
                    _vitalSignsRepository.Insert(patientVisit.VitalSigns);
                }
                else
                {
                    vitalSigns.BloodPressure = patientVisit.VitalSigns.ElementAtOrDefault(0).BloodPressure;
                    vitalSigns.Glucose = patientVisit.VitalSigns.ElementAtOrDefault(0).Glucose;
                    vitalSigns.HeartRate = patientVisit.VitalSigns.ElementAtOrDefault(0).HeartRate;
                    vitalSigns.Height = patientVisit.VitalSigns.ElementAtOrDefault(0).Height;
                    vitalSigns.O2Saturation = patientVisit.VitalSigns.ElementAtOrDefault(0).O2Saturation;
                    vitalSigns.RespirationRate = patientVisit.VitalSigns.ElementAtOrDefault(0).RespirationRate;
                    vitalSigns.Temprature = patientVisit.VitalSigns.ElementAtOrDefault(0).Temprature;
                    vitalSigns.Weight = patientVisit.VitalSigns.ElementAtOrDefault(0).Weight;
                    vitalSigns.UpdatedDate = DateTime.Now;
                    _vitalSignsRepository.Update(vitalSigns);
                    patientVisit.VitalSigns.Clear();
                    patientVisit.VitalSigns.Add(vitalSigns);
                }
            }
        }

        #endregion
        #endregion


    }
}
