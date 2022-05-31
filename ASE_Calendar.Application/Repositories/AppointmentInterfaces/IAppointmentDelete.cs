using System;

namespace ASE_Calendar.Application.Repositories.AppointmentInterfaces
{
    public interface IAppointmentDelete
    {
        bool DeleteAppointment(Guid appointmentGuid);
    }
}