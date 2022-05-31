using System;

namespace ASE_Calendar.Application.Repositories.AppointmentInterfaces
{
    public interface IAppointmentDeleteRepository
    {
        bool DeleteAppointment(Guid appointmentGuid);
    }
}