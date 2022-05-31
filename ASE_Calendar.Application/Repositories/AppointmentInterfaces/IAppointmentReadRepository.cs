using System;
using System.Collections.Generic;
using ASE_Calendar.Domain.Entities;

namespace ASE_Calendar.Application.Repositories.AppointmentInterfaces
{
    public interface IAppointmentReadRepository
    {
        Dictionary<int, Dictionary<int, AppointmentEntity>> ReturnAllAppointmentsDict();
        Dictionary<int, Dictionary<int, AppointmentEntity>> ReturnAllAppointmentsDictSelectedMonth(DateTime selectedDate);
    }
}