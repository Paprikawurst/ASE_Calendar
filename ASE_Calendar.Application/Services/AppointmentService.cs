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

        public static string DeleteAnAppointment(Guid appointmentGuid)
        {
            AppointmentRepository appointmentRepository = new();

            return appointmentRepository.DeleteAppointment(appointmentGuid);
        }

        public static string ChangeDescription(Guid appointmentGuid, string description)
        {
            AppointmentRepository appointmentRepository = new();

            return appointmentRepository.ChangeDesciption(appointmentGuid, description);
        }

        public static string ChangeDate(Guid appointmentGuid, DateTime newDate)
        {
            AppointmentRepository appointmentRepository = new();

            return appointmentRepository.ChangeDate(appointmentGuid, newDate);
        }

        public static bool CheckIfTimeSlotIsFree(DateTime Date, int timeSlot)
        {
            AppointmentRepository appointmentRepository = new();
            var appointmentDict =
                appointmentRepository.ReturnAllAppointmentDict(Date);


            for (var i = 1; i <= CalendarHelperService.GetMaxMonthDayInt(Date.Month, Date.Year); i++)
            {
                if (appointmentDict.ContainsKey(i))
                {
                    if (appointmentDict[i].ContainsKey(timeSlot))
                    {
                        return false;
                    }
                }
            }

            return true;
        }
    }
}