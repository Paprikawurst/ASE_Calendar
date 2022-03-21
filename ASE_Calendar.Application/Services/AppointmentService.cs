using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ASE_Calendar.Application.Repositories;
using ASE_Calendar.Domain.Entities;

namespace ASE_Calendar.Application.Services
{
    public class AppointmentService
    {
        public AppointmentEntity Appointment { get; set; }

        public AppointmentService()
        {
        }

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

        public static string ChanngeDescription(Guid appointmentGuid, string description)
        {
            AppointmentRepository appointmentRepository = new();

            return appointmentRepository.ChangeDesciption(appointmentGuid, description);
        }

        public static string ChanngeDate(Guid appointmentGuid, DateTime newDate)
        {
            AppointmentRepository appointmentRepository = new();

            return appointmentRepository.ChangeDate(appointmentGuid, newDate);
        }
    }
}