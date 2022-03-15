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

        public string ReadFromJsonFileReturnString()
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
        public Dictionary<int, AppointmentEntity> ReadFromJsonFileReturnAppointmentDict(DateTime selectedDate)
        {
  
            int i = 0;
            Dictionary<int, AppointmentEntity> appointmentDict = new Dictionary<int, AppointmentEntity>();
            appointmentDict.Clear();

            if (File.Exists(AppDomain.CurrentDomain.BaseDirectory + "ASECalendarAppointments.json"))
            {
                string json = File.ReadAllText(AppDomain.CurrentDomain.BaseDirectory + "ASECalendarAppointments.json");
                string[] jsonSplit = json.Split("\n");

                foreach (var subString in jsonSplit)
                {
                    var Appointment = JsonConvert.DeserializeObject<AppointmentEntity>(subString);

                    if (Appointment != null && Appointment.AppointmentData.Date.Month == selectedDate.Month && Appointment.AppointmentData.Date.Year == selectedDate.Year)
                    {
                        if (Appointment.UserId.value == User.userId.value)
                        {
                            appointmentDict.Add(Appointment.AppointmentData.Date.Day, Appointment);
                            i++;
                        }
                    }
                }
            }

            return appointmentDict;

        }
    }
}
