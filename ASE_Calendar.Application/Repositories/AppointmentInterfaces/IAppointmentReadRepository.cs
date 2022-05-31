using ASE_Calendar.Domain.Entities;
using System;
using System.Collections.Generic;

namespace ASE_Calendar.Application.Repositories
{
    public interface IAppointmentReadRepository
    {
        Dictionary<int, Dictionary<int, AppointmentEntity>> ReturnAllAppointmentsDict();
        Dictionary<int, Dictionary<int, AppointmentEntity>> ReturnAllAppointmentsDictSelectedMonth(DateTime selectedDate);
    }
}