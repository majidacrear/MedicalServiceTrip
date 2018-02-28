using Core.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Mapping
{
    public class OrganizationMap : MSTEntityTypeConfiguration<Organization>
    {
        public OrganizationMap()
        {
            this.ToTable("Organization");
            this.HasKey(e => e.Id);
            this.Property(e => e.OrganizationName).IsRequired().HasMaxLength(500);
            this.Property(e => e.CreatedDate).IsRequired();

        }
    }
}
