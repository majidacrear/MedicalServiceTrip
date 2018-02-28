using Core.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Mapping
{
    public class OrganizationPharmacyMap : MSTEntityTypeConfiguration<OrganizationPharmacy>
    {
        public OrganizationPharmacyMap()
        {
            this.ToTable("OrganizationPharmacy");
            this.HasKey(e => e.Id);
            this.Property(e => e.CreatedDate).IsRequired();
            this.HasRequired(e => e.Organization).WithMany().HasForeignKey(e => e.OrganizationId).WillCascadeOnDelete(false);
        }
    }
}
