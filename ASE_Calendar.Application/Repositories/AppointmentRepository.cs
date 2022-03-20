using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ASE_Calendar.Application.Shared;
using ASE_Calendar.Domain.Entities;
using Newtonsoft.Json;

namespace ASE_Calendar.Application.Repositories
{
    public class AppointmentRepository
    {
        private UserEntity User { get; set; }
        private AppointmentEntity _appointment;

        public AppointmentRepository(UserEntity User)
        {
            this.User = User;
        }

        public AppointmentRepository(AppointmentEntity Appointment)
        {
            this._appointment = Appointment;
            CreateAppointment();
        }

        public string ReadFromJsonFileReturnString()
        {
            if (File.Exists(AppDomain.CurrentDomain.BaseDirectory + "ASECalendarAppointments.json"))
            {
                string json = File.ReadAllText(AppDomain.CurrentDomain.BaseDirectory + "ASECalendarAppointments.json");
                string[] jsonSplit = json.Split("\n");
                string appointmentsString = null;

                foreach (var subString in jsonSplit)
                {
                    var customJsonConverter = new CustomJsonConverter<AppointmentEntity>();
                    //TODO: neues Deserialize implementieren
                    var appointment = customJsonConverter.DeserializeObject(subString);

                    if (appointment != null)
                    {
                        if (appointment.UserId.Value == User.userId.Value)
                        {
                            appointmentsString = appointmentsString + appointment.AppointmentData.Date + " " +
                                                 appointment.AppointmentData.TimeSlot + "\n";
                        }
                    }
                }

                return appointmentsString;

            }

            return null;
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
                    //TODO: neues Deserialize implementieren
                    var customJsonConverter = new CustomJsonConverter<AppointmentEntity>();
                    var appointment = customJsonConverter.DeserializeObject(subString);

                    if (appointment != null && appointment.AppointmentData.Date.Month == selectedDate.Month && appointment.AppointmentData.Date.Year == selectedDate.Year)
                    {
                        if (appointment.UserId.Value == User.userId.Value)
                        {
                            appointmentDict.Add(appointment.AppointmentData.Date.Day, appointment);
                            i++;
                        }
                    }
                }
            }

            return appointmentDict;

        }

        private void CreateAppointment()
        {
            //TODO: neues Serialize
            var customJsonConverter = new CustomJsonConverter<AppointmentEntity>();
            var json = customJsonConverter.SerializeObject(_appointment);

            File.AppendAllText(AppDomain.CurrentDomain.BaseDirectory + "ASECalendarAppointments.json", json + "\n");

        }
    }
}
