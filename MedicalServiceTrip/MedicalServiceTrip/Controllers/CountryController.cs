using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MedicalServiceTrip.Model;
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
        public ServiceResponse<IEnumerable<Core.Domain.Country>>  GetCountryList()
        {
            var response = new ServiceResponse<IEnumerable<Core.Domain.Country>>();
            try
            {
                response.Model = _countryService.GetCountryList();
                response.Success = true;
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = ex.Message;
            }
            return response;
        }

        #endregion
    }
}