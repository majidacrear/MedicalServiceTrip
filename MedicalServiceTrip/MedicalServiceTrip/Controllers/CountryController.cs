using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Service.Country;

namespace MedicalServiceTrip.Controllers
{
    [Produces("application/json")]
    [Route("api/Country/[action]")]
    public class CountryController : BaseController
    {
        #region Fields

        private readonly ICountryService _countryService;
        #endregion

        #region Cors

        public CountryController(ICountryService countryService)
        {
            _countryService = countryService;
        }

        #endregion

        #region Methods

        [HttpGet]
        [ActionName("GetCountryList")]
        public IEnumerable<Core.Domain.Country> GetCountryList()
        {
            return _countryService.GetCountryList();
        }

        #endregion
    }
}