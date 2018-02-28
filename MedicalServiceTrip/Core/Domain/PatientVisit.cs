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

        public Patient Patient { get; set; }
    }
}
