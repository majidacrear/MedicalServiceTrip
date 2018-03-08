using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MedicalServiceTrip.Model
{
    public class ServiceResponse<T>
    {
        public T Model { get; set; }
        public bool Success { get; set; }

        public string Message { get; set; }
    }
}
