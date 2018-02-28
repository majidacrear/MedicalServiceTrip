using Core.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Mapping
{
    public class CountryMap : MSTEntityTypeConfiguration<Country>
    {
        public CountryMap()
        {
            this.ToTable("Country");
            this.HasKey(e => e.Id);
            this.Property(c => c.Name).IsRequired().HasMaxLength(100);
            this.Property(c => c.TwoLetterIsoCode).HasMaxLength(2);
            this.Property(c => c.ThreeLetterIsoCode).HasMaxLength(3);
        }
    }
}
