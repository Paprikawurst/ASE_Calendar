using ASE_Calendar.Domain.Entities;
using System;
using System.Collections.Generic;

namespace ASE_Calendar.Application.Repositories
{
    public interface IAppointmentRead
    {
        Dictionary<int, Dictionary<int, AppointmentEntity>> ReturnAllAppointmentsDict();
        Dictionary<int, Dictionary<int, AppointmentEntity>> ReturnAllAppointmentsDictSelectedMonth(DateTime selectedDate);
    }
}