using Core.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Patient
{
    public interface IPatientVisitService
    {
        PatientVisit AddPatientVisit(PatientVisit patientVisit);

        PatientVisit GetPatientVisitByPatientId(int patientId);

        IEnumerable<PatientVisit> GetPatientVisitHistory(int patientId);
    }
}
