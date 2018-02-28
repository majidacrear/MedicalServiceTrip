using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.Domain;

namespace Service.Organization
{
    public partial interface IOrganizationService
    {
        int RegisterOrganization(Core.Domain.Organization organization);

        bool CheckOrganizationExist(string name);

        Core.Domain.Organization GetOrganizationById(int id);

        Core.Domain.Organization GetOrganizationByName(string name);

        IEnumerable<Core.Domain.Organization> GetAllOrganization();
    }
}
