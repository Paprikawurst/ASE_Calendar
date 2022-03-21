using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ASE_Calendar.Application.Services;
using ASE_Calendar.Application.Shared;
using ASE_Calendar.Domain.Entities;
using Newtonsoft.Json;

namespace ASE_Calendar.Application.Repositories
{
    public class AppointmentRepository
    {
        private UserEntity User { get; set; }
        private AppointmentEntity _appointment;
        private CalendarHelperService _calendarHelper = new();

        public AppointmentRepository(UserEntity user)
        {
            User = user;
        }

        public AppointmentRepository(AppointmentEntity appointment)
        {
            _appointment = appointment;
            CreateAppointment();
        }

        public string ReadFromJsonFileReturnString()
        {
            if (!File.Exists(AppDomain.CurrentDomain.BaseDirectory + "ASECalendarAppointments.json"))
            {
                return null;
            }

            string json = File.ReadAllText(AppDomain.CurrentDomain.BaseDirectory + "ASECalendarAppointments.json");
            string[] jsonSplit = json.Split("\n");
            string appointmentsString = null;

            foreach (var subString in jsonSplit)
            {
                var customJsonConverter = new CustomJsonConverter<AppointmentEntity>();
                var appointment = customJsonConverter.DeserializeObject(subString);

                if (appointment != null)
                {
                    if (appointment.UserId.Value == User.UserId.Value)
                    {
                        appointmentsString = appointmentsString + appointment.AppointmentData.Date.ToLongDateString() +
                                             " " +
                                             _calendarHelper.TimeSlotToTimeStamp(appointment.AppointmentData.TimeSlot) +
                                             " " + appointment.AppointmentData.Description + "\n";
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
                    var customJsonConverter = new CustomJsonConverter<AppointmentEntity>();
                    var appointment = customJsonConverter.DeserializeObject(subString);

                    if (appointment != null && appointment.AppointmentData.Date.Month == selectedDate.Month &&
                        appointment.AppointmentData.Date.Year == selectedDate.Year)
                    {
                        if (appointment.UserId.Value == User.UserId.Value)
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
            var customJsonConverter = new CustomJsonConverter<AppointmentEntity>();
            var json = customJsonConverter.SerializeObject(_appointment);

            File.AppendAllText(AppDomain.CurrentDomain.BaseDirectory + "ASECalendarAppointments.json", json + "\n");
        }
    }
}