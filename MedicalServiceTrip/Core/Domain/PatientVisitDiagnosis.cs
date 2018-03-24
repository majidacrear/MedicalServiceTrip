using Newtonsoft.Json;
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
        //[JsonIgnore]
        public PatientVisit PatientVisit { get; set; }

        [JsonIgnore]
        public CheifComplain Diagnosis { get; set; }
    }
}
