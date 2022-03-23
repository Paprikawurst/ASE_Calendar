using System;
using System.Collections.Generic;
using System.IO;
using ASE_Calendar.Application.Services;
using ASE_Calendar.Application.Shared;
using ASE_Calendar.Domain.Entities;

namespace ASE_Calendar.Application.Repositories
{
    public class AppointmentRepository
    {
        private readonly CalendarHelperService _calendarHelper = new();


        public string ReturnUserAppointmentString(UserEntity user)
        {
            if (!File.Exists(AppDomain.CurrentDomain.BaseDirectory + "ASECalendarAppointments.json"))
            {
                return null;
            }

            var json = File.ReadAllText(AppDomain.CurrentDomain.BaseDirectory + "ASECalendarAppointments.json");
            var jsonSplit = json.Split("\n");
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

            var json = File.ReadAllText(AppDomain.CurrentDomain.BaseDirectory + "ASECalendarAppointments.json");
            var jsonSplit = json.Split("\n");
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


        public Dictionary<int, Dictionary<int, AppointmentEntity>> ReturnAllAppointmentDict(DateTime selectedDate)
        {
            var i = 0;
            Dictionary<int, Dictionary<int, AppointmentEntity>> appointmentDict = new();
            appointmentDict.Clear();

            Dictionary<int, AppointmentEntity> test = new();

            if (File.Exists(AppDomain.CurrentDomain.BaseDirectory + "ASECalendarAppointments.json"))
            {
                var json = File.ReadAllText(AppDomain.CurrentDomain.BaseDirectory + "ASECalendarAppointments.json");
                var jsonSplit = json.Split("\n");

                foreach (var subString in jsonSplit)
                {
                    var customJsonConverter = new CustomJsonConverter<AppointmentEntity>();
                    var appointment = customJsonConverter.DeserializeObject(subString);

                    if (appointment != null && appointment.AppointmentData.Date.Month == selectedDate.Month &&
                        appointment.AppointmentData.Date.Year == selectedDate.Year)
                    {
                        if (appointmentDict.ContainsKey(appointment.AppointmentData.Date.Day))
                        {
                            appointmentDict[appointment.AppointmentData.Date.Day][
                                appointment.AppointmentData.TimeSlot] = appointment;
                        }
                        else
                        {
                            test[appointment.AppointmentData.TimeSlot] = appointment;
                            appointmentDict.Add(appointment.AppointmentData.Date.Day, test);
                        }

                        i++;
                    }
                }

                File.AppendAllText(AppDomain.CurrentDomain.BaseDirectory + "test.json", appointmentDict + "\n");
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

            var json = File.ReadAllText(AppDomain.CurrentDomain.BaseDirectory + "ASECalendarAppointments.json");
            var jsonSplit = json.Split("\n");
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

            return "";
        }

        public string ChangeDesciption(Guid appointmentGuid, string newDescription)
        {
            if (!File.Exists(AppDomain.CurrentDomain.BaseDirectory + "ASECalendarAppointments.json"))
            {
                return null;
            }

            var json = File.ReadAllText(AppDomain.CurrentDomain.BaseDirectory + "ASECalendarAppointments.json");
            var jsonSplit = json.Split("\n");
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

            var json = File.ReadAllText(AppDomain.CurrentDomain.BaseDirectory + "ASECalendarAppointments.json");
            var jsonSplit = json.Split("\n");
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