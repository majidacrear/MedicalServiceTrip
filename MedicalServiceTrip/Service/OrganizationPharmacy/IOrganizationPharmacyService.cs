using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.OrganizationPharmacy
{
    public interface IOrganizationPharmacyService
    {
        Core.Domain.OrganizationPharmacy AddOrganizationPharmacy(Core.Domain.OrganizationPharmacy organizationPharmacy);

        IEnumerable<Core.Domain.OrganizationPharmacy> GetOrganizationPharmacy(int organizationId);
    }
}
