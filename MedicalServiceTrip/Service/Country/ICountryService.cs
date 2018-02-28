using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Country
{
    public interface ICountryService
    {
        IEnumerable<Core.Domain.Country> GetCountryList();
    }
}
