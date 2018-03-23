using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Domain
{
    public partial class VitalSigns : BaseEntity
    {
        public float HeartRate { get; set; }

        public string BloodPressure { get; set; }

        public string RespirationRate { get; set; }

        public string O2Saturation { get; set; }

        public float Temprature { get; set; }

        public string Glucose { get; set; }

        public string Weight { get; set; }

        public string Height { get; set; }

        public int PatientVisitId { get; set; }

        
        public PatientVisit PatientVisit { get; set; }


    }
}
