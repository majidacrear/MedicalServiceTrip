using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Domain
{
    public partial class Patient : BaseEntity
    {
        public int PatientIdNumber { get; set; }

        public string FullName { get; set; }

        public int Age { get; set; }

        public int GenderId { get; set; }

        public string PhoneNumber { get; set; }

        public string Location { get; set; }

        public int OrganizationId { get; set; }

        public string ImagePath { get; set; }

        /// <summary>
        /// Doctor Id
        /// </summary>
        public  int DoctorId { get; set; }

        [JsonIgnore]
        public virtual Users Doctor { get; set; }
        [JsonIgnore]
        public virtual Gender Gender { get; set; }
        [JsonIgnore]
        public virtual Organization Organization { get; set; }

        public virtual ICollection<PatientVisit> PatientVisit { get; set; }

        public virtual string PatientVisitStatus { get; set; }
    }
}
