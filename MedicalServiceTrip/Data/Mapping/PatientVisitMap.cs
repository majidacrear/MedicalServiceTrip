using Core.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Mapping
{
    public class PatientVisitMap : MSTEntityTypeConfiguration<PatientVisit>
    {
        public PatientVisitMap()
        {
            this.ToTable("PatientVisit");
            this.HasKey(e => e.Id);
            this.Property(e => e.PatientHistory).IsOptional().HasMaxLength(8000);
            this.Property(e => e.CreatedDate).IsRequired();
            this.HasRequired(e => e.Patient).WithMany(e => e.PatientVisit).HasForeignKey(e => e.PatientId).WillCascadeOnDelete(false);
        }
    }
}
