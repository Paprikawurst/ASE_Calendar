using ASE_Calendar.Domain.Entities;
using System;
using System.Collections.Generic;

namespace ASE_Calendar.Application.Repositories
{
    public interface IAppointmentDeleteRepository
    {
        bool DeleteAppointment(Guid appointmentGuid);
    }
}