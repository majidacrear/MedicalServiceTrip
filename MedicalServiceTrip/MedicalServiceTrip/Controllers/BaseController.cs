using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace MedicalServiceTrip.Controllers
{
    [Route("api/[controller]")]
    public class BaseController : Controller
    {

        #region Methods
        protected string GetErrorMessageDetail(Exception ex)
        {
            return GetExceptionMessage(ex);
        }

        private String GetExceptionMessage(Exception ex)
        {
            String message=string.Empty;
            if (ex.InnerException != null)
                message += GetExceptionMessage(ex.InnerException);
            else
                message = ex.Message;
            return message;
        }
        #endregion
    }
}
