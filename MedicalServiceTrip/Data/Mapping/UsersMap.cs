using Core.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Mapping
{
    public class UsersMap : MSTEntityTypeConfiguration<Users>
    {
        public UsersMap()
        {
            this.ToTable("Users");
            this.HasKey(e => e.Id);
            this.Property(e => e.FullName).IsRequired().HasMaxLength(500);
            this.Property(e => e.Email).IsRequired().HasMaxLength(500);
            this.Property(e => e.Password).IsRequired().HasMaxLength(200);
            this.Property(e => e.PasswordSalt).IsRequired().HasMaxLength(100);
            this.Property(e => e.TypeOfProfessional).IsRequired().HasMaxLength(100);
            this.Property(e => e.Specialty).IsRequired().HasMaxLength(100);
            this.Property(e => e.DeviceNumber).IsRequired().HasMaxLength(1000);
            this.Property(e => e.PinCode).IsRequired();
            this.Property(e => e.IsOrganizationAdmin).IsRequired();
            this.Property(e => e.IsActive).IsRequired();
            this.Property(e => e.CreatedDate).IsRequired();
            this.HasOptional(e => e.Organization).WithMany().HasForeignKey(e => e.OrganizationId).WillCascadeOnDelete(false);
            this.HasRequired(e => e.Country).WithMany().HasForeignKey(e => e.CountryId).WillCascadeOnDelete(false);
            this.Ignore(e => e.ApiKey);
        }
    }
}
