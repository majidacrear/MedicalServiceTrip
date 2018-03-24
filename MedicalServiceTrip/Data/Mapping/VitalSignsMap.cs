using Core.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Mapping
{
    public class VitalSignsMap : MSTEntityTypeConfiguration<VitalSigns>
    {
        public VitalSignsMap()
        {
            this.ToTable("VitalSigns");
            this.HasKey(e => e.Id);
            this.Property(e => e.CreatedDate).IsRequired();
            this.HasRequired(e => e.PatientVisit).WithMany(e => e.VitalSigns).HasForeignKey(e => e.PatientVisitId).WillCascadeOnDelete(false);
        }
    }
}
