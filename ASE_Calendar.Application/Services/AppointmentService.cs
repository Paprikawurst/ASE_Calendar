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
            AppointmentRepository appointmentRepository = new(appointment);
        }

        public static string LoadAppointments(UserEntity user)
        {
            AppointmentRepository appointmentRepository = new(user);
            
            return appointmentRepository.ReadFromJsonFileReturnString(); 
        }
    }
}
