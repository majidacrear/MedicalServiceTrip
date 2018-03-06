using Core.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Mapping
{
    public class DiagnosisMap : MSTEntityTypeConfiguration<Diagnosis>
    {
        public DiagnosisMap()
        {
            this.ToTable("Diagnosis");
            this.HasKey(e => e.Id);
            this.Property(e => e.Name).IsRequired();
            this.Property(e => e.CreatedDate).IsRequired();
            this.HasRequired(e => e.Organization).WithMany().HasForeignKey(e => e.OrganizationId).WillCascadeOnDelete(false);
        }
    }
}
