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

        public virtual Patient Patient { get; set; }

        public virtual VitalSigns VitalSigns { get; set; }

        public virtual IEnumerable<PatientVisitDiagnosis> PatientVisitDiagnosis { get; set; }

        public virtual IEnumerable<PatientVisitCheifComplain> PatientVisitCheifComplain { get; set; }
    }
}
