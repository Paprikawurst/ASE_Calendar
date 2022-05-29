using ASE_Calendar.Domain.Entities;
using System;
using System.Collections.Generic;

namespace ASE_Calendar.Application.Repositories
{
    public interface IAppointmentRepository
    {
        bool ChangeDate(Guid appointmentGuid, DateTime newDate);
        bool ChangeDescription(Guid appointmentGuid, string newDescription);
        void CreateAppointment(AppointmentEntity appointmentEntity);
        bool DeleteAppointment(Guid appointmentGuid);
        Dictionary<int, Dictionary<int, AppointmentEntity>> ReturnAllAppointmentsDict();
        Dictionary<int, Dictionary<int, AppointmentEntity>> ReturnAllAppointmentsDictSelectedMonth(DateTime selectedDate);
    }
}