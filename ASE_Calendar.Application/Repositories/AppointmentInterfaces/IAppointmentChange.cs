using ASE_Calendar.Domain.Entities;
using System;
using System.Collections.Generic;

namespace ASE_Calendar.Application.Repositories
{
    public interface IAppointmentChange
    {
        bool ChangeDate(Guid appointmentGuid, DateTime newDate);
        bool ChangeDescription(Guid appointmentGuid, string newDescription);
    }
}