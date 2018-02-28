using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Domain
{
    public partial class PatientVisitDiagnosis : BaseEntity
    {
        public int PatientVisitId { get; set; }

        public int DiagnosisId { get; set; }

        public PatientVisit PatientVisit { get; set; }

        public Diagnosis Diagnosis { get; set; }
    }
}
