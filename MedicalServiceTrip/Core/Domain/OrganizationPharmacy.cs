using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Domain
{
    public partial class OrganizationPharmacy : BaseEntity
    {
        public string MedicineName { get; set; }

        public int AvailableStock { get; set; }

        public int OrganizationId { get; set; }

        public Organization Organization { get; set; }
    }
}
