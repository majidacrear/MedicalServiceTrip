using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Domain
{
    public partial class PatientVisitCheifComplain : BaseEntity
    {
        public int PatientVisitId { get; set; }

        public int CheifComplainId { get; set; }

        public PatientVisit PatientVisit { get; set; }

        public CheifComplain CheifComplain { get; set; }
    }
}
