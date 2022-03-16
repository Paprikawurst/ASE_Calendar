using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ASE_Calendar.Domain.Entities;
using Newtonsoft.Json;

namespace ASE_Calendar.Application.Repositories
{
    class AppointmentRepository
    {
        public UserEntity UserEntity { get; set; }
        public AppointmentEntity AppointmentEntity;

        public AppointmentRepository(UserEntity user)
        {
            UserEntity = user;

        }

        public AppointmentRepository(AppointmentEntity appointment)
        {
            AppointmentEntity = appointment;
            AppointmentToJson();
        }


        private void AppointmentToJson()
        {
            var json = JsonConvert.SerializeObject(AppointmentEntity);

            File.AppendAllText(AppDomain.CurrentDomain.BaseDirectory + "ASECalendarAppointments.json", json + "\n");

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
                    if (Appointment.UserId.Value == UserEntity.userId.Value)
                    {
                        appointmentsString = appointmentsString + Appointment.AppointmentData.Date + " " + Appointment.AppointmentData.TimeSlot + "\n";
                    }
                }
            }

            return appointmentsString;

        }
        public Dictionary<int, AppointmentEntity> ReadFromJsonFileReturnAppointmentDict(DateTime selectedDate)
        {

            int i = 0;
            Dictionary<int, AppointmentEntity> appointmentDict = new();
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
                        if (Appointment.UserId.Value == UserEntity.userId.Value)
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
