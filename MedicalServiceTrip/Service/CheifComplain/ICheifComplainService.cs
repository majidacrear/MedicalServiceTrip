using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.CheifComplain
{
    public interface ICheifComplainService
    {
        Core.Domain.CheifComplain AddCheifComplain(Core.Domain.CheifComplain cheifComplain);

        IEnumerable<Core.Domain.CheifComplain> GetCheifComplainList(int organizationId);
    }
}
