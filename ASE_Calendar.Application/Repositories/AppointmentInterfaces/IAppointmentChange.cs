using System;

namespace ASE_Calendar.Application.Repositories.AppointmentInterfaces
{
    public interface IAppointmentChange
    {
        bool ChangeDate(Guid appointmentGuid, DateTime newDate);
        bool ChangeDescription(Guid appointmentGuid, string newDescription);
    }
}