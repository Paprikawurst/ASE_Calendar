using System;
using ASE_Calendar.Application.Repositories;

namespace ASE_Calendar.ConsoleUI.Services
{
    public class AppointmentService
    {
        public static bool CheckIfTimeSlotIsFree(DateTime Date, int timeSlot, int selectedDay)
        {
            AppointmentRepository appointmentRepository = new();
            var appointmentDict =
                appointmentRepository.ReturnAllAppointmentsDictSelectedMonth(Date);


            for (var i = 1; i <= CalendarHelperService.GetMaxMonthDayInt(Date.Month, Date.Year); i++)
            {
                if (appointmentDict.ContainsKey(selectedDay))
                {
                    if (appointmentDict[selectedDay].ContainsKey(timeSlot))
                    {
                        return false;
                    }
                }
            }

            return true;
        }
    }
}