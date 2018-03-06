using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MedicalServiceTrip.Controllers
{
    [Produces("application/json")]
    [Route("api/PatientVisit/[action]")]
    public class PatientVisitController : BaseController
    {
    }
}