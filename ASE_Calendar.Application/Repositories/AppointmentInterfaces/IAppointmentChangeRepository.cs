using System;

namespace ASE_Calendar.Application.Repositories.AppointmentInterfaces
{
    public interface IAppointmentChangeRepository
    {
        bool ChangeDate(Guid appointmentGuid, DateTime newDate);
        bool ChangeDescription(Guid appointmentGuid, string newDescription);
    }
}