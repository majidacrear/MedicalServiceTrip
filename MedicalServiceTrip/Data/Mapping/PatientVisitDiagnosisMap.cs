using Core.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Mapping
{
    public class PatientVisitDiagnosisMap : MSTEntityTypeConfiguration<PatientVisitDiagnosis>
    {
        public PatientVisitDiagnosisMap()
        {
            this.ToTable("PatientVisitDiagnosis");
            this.HasKey(e => e.Id);
            this.Property(e => e.CreatedDate).IsRequired();
            this.HasRequired(e => e.Diagnosis).WithMany().HasForeignKey(e => e.DiagnosisId).WillCascadeOnDelete(false);
            this.HasRequired(e => e.PatientVisit).WithMany(e=>e.PatientVisitDiagnosis).HasForeignKey(e => e.PatientVisitId).WillCascadeOnDelete(false);
        }
    }
}
