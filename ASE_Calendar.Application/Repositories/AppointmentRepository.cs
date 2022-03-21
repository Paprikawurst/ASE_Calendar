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
        
        private CalendarHelperService _calendarHelper = new();


        public string ReturnUserAppointmentString(UserEntity user)
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
                    if (appointment.UserId.Value == user.UserId.Value)
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

        public string ReturnAllAppointmentsString()
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
                    
                    appointmentsString = appointmentsString + appointment.AppointmentId.Value + 
                                         " " + 
                                         appointment.AppointmentData.Date.ToLongDateString() +
                                         " " +
                                         _calendarHelper.TimeSlotToTimeStamp(appointment.AppointmentData.TimeSlot) +
                                         " " + appointment.AppointmentData.Description + "\n";
                    
                }
            }

            return appointmentsString;
        }

        public Dictionary<int, AppointmentEntity> ReturnAppointmentDict(DateTime selectedDate, UserEntity user)
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
                        if (appointment.UserId.Value == user.UserId.Value)
                        {
                            appointmentDict.Add(appointment.AppointmentData.Date.Day, appointment);
                            i++;
                        }
                    }
                }
            }

            return appointmentDict;
        }

        public void CreateAppointment(AppointmentEntity appointmentInput)
        {
            var customJsonConverter = new CustomJsonConverter<AppointmentEntity>();
            var json = customJsonConverter.SerializeObject(appointmentInput);

            File.AppendAllText(AppDomain.CurrentDomain.BaseDirectory + "ASECalendarAppointments.json", json + "\n");
        }

        public string DeleteAppointment(Guid appointmentGuid)
        {
            if (!File.Exists(AppDomain.CurrentDomain.BaseDirectory + "ASECalendarAppointments.json"))
            {
                return null;
            }

            string json = File.ReadAllText(AppDomain.CurrentDomain.BaseDirectory + "ASECalendarAppointments.json");
            string[] jsonSplit = json.Split("\n");
            var i = 0;

            foreach (var subString in jsonSplit)
            {
                var customJsonConverter = new CustomJsonConverter<AppointmentEntity>();
                var appointment = customJsonConverter.DeserializeObject(subString);

                if (appointment != null)
                {
                    if (appointment.AppointmentId.Value == appointmentGuid)
                    {
                        jsonSplit[i] = "";
                    }
                }

                i++;
            }

            File.Delete(AppDomain.CurrentDomain.BaseDirectory + "ASECalendarAppointments.json");

            foreach (var subString in jsonSplit)
            {
                if (subString != "")
                {
                    var customJsonConverter = new CustomJsonConverter<AppointmentEntity>();
                    var appointment = customJsonConverter.DeserializeObject(subString);
                    CreateAppointment(appointment);
                }
            }

            return"";
        }

        public string ChangeDesciption(Guid appointmentGuid, string newDescription)
        {
            if (!File.Exists(AppDomain.CurrentDomain.BaseDirectory + "ASECalendarAppointments.json"))
            {
                return null;
            }

            string json = File.ReadAllText(AppDomain.CurrentDomain.BaseDirectory + "ASECalendarAppointments.json");
            string[] jsonSplit = json.Split("\n");
            AppointmentEntity changedAppointment = null;
            var i = 0;

            foreach (var subString in jsonSplit)
            {
                var customJsonConverter = new CustomJsonConverter<AppointmentEntity>();
                var appointment = customJsonConverter.DeserializeObject(subString);

                if (appointment != null)
                {
                    if (appointment.AppointmentId.Value == appointmentGuid)
                    {
                        jsonSplit[i] = "";
                        changedAppointment = new AppointmentEntity(appointment.AppointmentData.Date,
                            appointment.AppointmentData.TimeSlot, appointment.UserId, appointment.AppointmentId.Value,
                            newDescription);
                    }
                }

                i++;
            }

            File.Delete(AppDomain.CurrentDomain.BaseDirectory + "ASECalendarAppointments.json");

            foreach (var subString in jsonSplit)
            {
                if (subString != "")
                {
                    var customJsonConverter = new CustomJsonConverter<AppointmentEntity>();
                    var appointment = customJsonConverter.DeserializeObject(subString);
                    CreateAppointment(appointment);
                }

            }

            CreateAppointment(changedAppointment);
            return "";
        }

        public string ChangeDate(Guid appointmentGuid, DateTime newDate)
        {
            if (!File.Exists(AppDomain.CurrentDomain.BaseDirectory + "ASECalendarAppointments.json"))
            {
                return null;
            }

            string json = File.ReadAllText(AppDomain.CurrentDomain.BaseDirectory + "ASECalendarAppointments.json");
            string[] jsonSplit = json.Split("\n");
            AppointmentEntity changedAppointment = null;
            var i = 0;

            foreach (var subString in jsonSplit)
            {
                var customJsonConverter = new CustomJsonConverter<AppointmentEntity>();
                var appointment = customJsonConverter.DeserializeObject(subString);

                if (appointment != null)
                {
                    if (appointment.AppointmentId.Value == appointmentGuid)
                    {
                        jsonSplit[i] = "";
                        changedAppointment = new AppointmentEntity(newDate,
                            appointment.AppointmentData.TimeSlot, appointment.UserId, appointment.AppointmentId.Value,
                            appointment.AppointmentData.Description);
                    }
                }

                i++;
            }

            File.Delete(AppDomain.CurrentDomain.BaseDirectory + "ASECalendarAppointments.json");

            foreach (var subString in jsonSplit)
            {
                if (subString != "")
                {
                    var customJsonConverter = new CustomJsonConverter<AppointmentEntity>();
                    var appointment = customJsonConverter.DeserializeObject(subString);
                    CreateAppointment(appointment);
                }

            }

            CreateAppointment(changedAppointment);
            return "";
        }
    }
}