using ASE_Calendar.Domain.Entities;
using System;
using System.Collections.Generic;

namespace ASE_Calendar.Application.Repositories
{
    public interface IAppointmentChangeRepository
    {
        bool ChangeDate(Guid appointmentGuid, DateTime newDate);
        bool ChangeDescription(Guid appointmentGuid, string newDescription);
    }
}