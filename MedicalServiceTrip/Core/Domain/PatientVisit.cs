using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Domain
{
    public partial class PatientVisit : BaseEntity
    {
        public string PatientHistory { get; set; }

        public int PatientId { get; set; }

        public bool VisitCompleted { get; set; }

        [JsonIgnore]
        public virtual Patient Patient { get; set; }

        public virtual ICollection<VitalSigns> VitalSigns { get; set; }

        public virtual ICollection<PatientVisitDiagnosis> PatientVisitDiagnosis { get; set; }

        public virtual ICollection<PatientVisitCheifComplain> PatientVisitCheifComplain { get; set; }

        public virtual ICollection<PatientVisitPrescription> PatientVisitPrescription { get; set; }
    }
}
