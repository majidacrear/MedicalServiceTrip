using Core.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Mapping
{
    public class CheifComplainMap : MSTEntityTypeConfiguration<CheifComplain>
    {
        public CheifComplainMap()
        {
            this.ToTable("CheifComplain");
            this.HasKey(e => e.Id);
            this.Property(e => e.CreatedDate).IsRequired();
            this.HasOptional(e => e.ParentCheifComplain).WithMany().HasForeignKey(e => e.ParentCheifComplainId).WillCascadeOnDelete(false);
            this.HasRequired(e => e.Organization).WithMany().HasForeignKey(e => e.OrganizationId).WillCascadeOnDelete(false);
        }
    }
}
