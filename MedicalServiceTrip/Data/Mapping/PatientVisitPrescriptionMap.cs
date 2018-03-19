using Core.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Mapping
{
    public class PatientVisitPrescriptionMap : MSTEntityTypeConfiguration<PatientVisitPrescription>
    {
        public PatientVisitPrescriptionMap()
        {
            this.ToTable("PatientVisitPrescription");
            this.HasKey(e => e.Id);
            this.Property(e => e.CreatedDate).IsRequired();
            this.HasRequired(e => e.OrganizationPharmacy).WithMany().HasForeignKey(e => e.OrganizationPharmacyId).WillCascadeOnDelete(false);
            this.HasRequired(e => e.PatientVisit).WithMany().HasForeignKey(e => e.PatientVisitId).WillCascadeOnDelete(false);
        }
    }
}
