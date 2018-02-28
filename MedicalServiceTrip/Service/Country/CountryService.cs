using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.Data;
using Core.Domain;

namespace Service.Country
{
    public class CountryService : ICountryService
    {
        #region Fields
        private readonly IRepository<Core.Domain.Country> _countryRepositroy;
        #endregion

        #region Cors

        public CountryService(IRepository<Core.Domain.Country> countryRepositroy)
        {
            this._countryRepositroy = countryRepositroy;
        }
        
        #endregion

        public IEnumerable<Core.Domain.Country> GetCountryList()
        {
            return this._countryRepositroy.Table.ToList();
        }
    }
}
