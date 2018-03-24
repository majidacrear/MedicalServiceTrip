using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Domain
{
    public class PatientVisitPrescription : BaseEntity
    {
        public int PatientVisitId { get; set; }

        public int OrganizationPharmacyId { get; set; }

        public int Quantity { get; set; }

        public string Description { get; set; }

        //[JsonIgnore]
        public PatientVisit PatientVisit { get; set; }

        public OrganizationPharmacy OrganizationPharmacy { get; set; }
    }
}
