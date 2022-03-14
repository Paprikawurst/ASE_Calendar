using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ASE_Calendar.Domain.Entities;

namespace ASE_Calendar.Application.Repositories
{
    class SaveAppointment
    {
        private AppointmentEntity Appointment;
        public SaveAppointment(AppointmentEntity Appointment)
        {
            this.Appointment = Appointment;
            AppointmentToJson();
        }

        private void AppointmentToJson()
        {
            var json = JsonConvert.SerializeObject(Appointment);

            File.AppendAllText(AppDomain.CurrentDomain.BaseDirectory + "ASECalendarAppointments.json", json + "\n");

        }
    }
}
