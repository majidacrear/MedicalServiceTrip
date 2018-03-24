using Core.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Mapping
{
    public class PatientVisitCheifComplainMap : MSTEntityTypeConfiguration<PatientVisitCheifComplain>
    {
        public PatientVisitCheifComplainMap()
        {
            this.ToTable("PatientVisitCheifComplain");
            this.HasKey(e => e.Id);
            this.Property(e => e.CreatedDate).IsRequired();
            this.HasRequired(e => e.CheifComplain).WithMany().HasForeignKey(e => e.CheifComplainId).WillCascadeOnDelete(false);
            this.HasRequired(e => e.PatientVisit).WithMany(e=>e.PatientVisitCheifComplain).HasForeignKey(e => e.PatientVisitId).WillCascadeOnDelete(false);
        }
    }
}
