using Core.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Mapping
{
    public class PatientMap : MSTEntityTypeConfiguration<Patient>
    {
        public PatientMap()
        {
            this.ToTable("Patient");
            this.HasKey(e => e.Id);
            this.Property(e => e.FullName).IsRequired().HasMaxLength(500);
            this.Property(e => e.PhoneNumber).IsRequired().HasMaxLength(20);
            this.Property(e => e.Location).IsRequired().HasMaxLength(500);
            this.Property(e => e.ImagePath).HasMaxLength(1000);
            this.Property(e => e.CreatedDate).IsRequired();
            this.HasRequired(e => e.Organization).WithMany().HasForeignKey(e => e.OrganizationId).WillCascadeOnDelete(false);
            this.HasRequired(e => e.Gender).WithMany().HasForeignKey(e => e.GenderId).WillCascadeOnDelete(false);
            this.HasRequired(e => e.Doctor).WithMany().HasForeignKey(e => e.DoctorId).WillCascadeOnDelete(false);
            this.Ignore(e => e.PatientVisitStatus);
        }
    }
}
