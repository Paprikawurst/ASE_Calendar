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
    class ReadAppointment
    {
        public UserEntity User { get; set; }

        public ReadAppointment(UserEntity User)
        {
            this.User = User;

        }

        public string ReadFromJsonFile()
        {
            string json = File.ReadAllText(AppDomain.CurrentDomain.BaseDirectory + "ASECalendarAppointments.json");
            string[] jsonSplit = json.Split("\n");
            string appointmentsString = null;

            foreach (var subString in jsonSplit)
            {
                var Appointment = JsonConvert.DeserializeObject<AppointmentEntity>(subString);
                
                if (Appointment != null)
                {
                    if (Appointment.UserId.value == User.userId.value)
                    {
                        appointmentsString = appointmentsString + Appointment.AppointmentData.Date + " " + Appointment.AppointmentData.timeSlot + "\n";
                    }
                }
            }

            return appointmentsString;

        }
    }
}
