using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ASE_Calendar.Application.Repositories
{
    class SaveAppointment
    {
        private Domain.Entities.AppointmentEntity Appointment;
        public SaveAppointment(Domain.Entities.AppointmentEntity Appointment)
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
