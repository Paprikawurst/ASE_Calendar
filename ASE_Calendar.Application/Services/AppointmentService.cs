using System;
using ASE_Calendar.Application.Repositories;
using ASE_Calendar.Domain.Entities;

namespace ASE_Calendar.Application.Services
{
    public class AppointmentService
    {
        public static void CreateAppointment(AppointmentEntity appointment)
        {
            AppointmentRepository appointmentRepository = new();
            appointmentRepository.CreateAppointment(appointment);
        }

        public static string LoadAppointments(UserEntity user)
        {
            AppointmentRepository appointmentRepository = new();

            return appointmentRepository.ReturnUserAppointmentString(user);
        }

        public static string LoadAllAppointments()
        {
            AppointmentRepository appointmentRepository = new();

            return appointmentRepository.ReturnAllAppointmentsString();
        }

        public static bool DeleteAnAppointment(Guid appointmentGuid)
        {
            AppointmentRepository appointmentRepository = new();

            return appointmentRepository.DeleteAppointment(appointmentGuid);
        }

        public static bool ChangeDescription(Guid appointmentGuid, string description)
        {
            AppointmentRepository appointmentRepository = new();

            return appointmentRepository.ChangeDescription(appointmentGuid, description);
        }

        public static bool ChangeDate(Guid appointmentGuid, DateTime newDate)
        {
            AppointmentRepository appointmentRepository = new();

            return appointmentRepository.ChangeDate(appointmentGuid, newDate);
        }

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