﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Patient
{
    public interface IPatientService
    {
        int AddPatient(Core.Domain.Patient patient);

        Core.Domain.Patient GetPatientById(int patientId);

        IEnumerable<Core.Domain.Patient> GetAllPatientByOrganizationAndUserId(int organizationnId,int userId);

        bool TransferPatient(int patientId, int toDoctorId);
    }
}
