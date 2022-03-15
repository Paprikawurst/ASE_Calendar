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

        public void createAppointment(AppointmentEntity appointment)
        {
            SaveAppointment saveAppointment = new SaveAppointment(appointment);
        }

        public string loadAppointments(UserEntity user)
        {
            ReadAppointment readAppointment = new ReadAppointment(user);
            
            return readAppointment.ReadFromJsonFileReturnString(); 
        }
    }
}
