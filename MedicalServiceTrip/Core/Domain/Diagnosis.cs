using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Domain
{
    public partial class Diagnosis : BaseEntity
    {
        public string Name { get; set; }

        public int OrganizationId { get; set; }

        public Organization Organization { get; set; }
    }
}
