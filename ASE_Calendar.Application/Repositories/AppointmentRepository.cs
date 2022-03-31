using System;
using System.Collections.Generic;
using System.IO;
using ASE_Calendar.Application.Services;
using ASE_Calendar.Application.Shared;
using ASE_Calendar.Domain.Entities;

namespace ASE_Calendar.Application.Repositories
{
    /// <summary>
    /// This repository manages the CRUD operations for the appointment entity.
    /// </summary>
    public class AppointmentRepository
    {
        private readonly CalendarHelperService _calendarHelper = new();
        private readonly CustomJsonConverter<AppointmentEntity> _customJsonConverter = new();

        /// <summary>
        /// Serializes an appointment entity to a json format and appends it to the ASECalendarAppointments.json
        /// </summary>
        /// <param name="appointmentEntity"></param>
        public void CreateAppointment(AppointmentEntity appointmentEntity)
        {
            var json = _customJsonConverter.SerializeObject(appointmentEntity);
            File.AppendAllText(AppDomain.CurrentDomain.BaseDirectory + "ASECalendarAppointments.json", json + "\n");
        }

        /// <summary>
        /// Reads existing entries from ASECalendarAppointments.json for a specific user
        /// </summary>
        /// <param name="user"></param>
        /// <returns>
        /// A formatted string with appointment entries.
        /// </returns>
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
                var appointmentEntity = _customJsonConverter.DeserializeObject(subString);

                if (appointmentEntity != null)
                {
                    if (appointmentEntity.UserId.Value == user.UserId.Value)
                    {
                        appointmentsString = appointmentsString + appointmentEntity.AppointmentData.Date.ToLongDateString() +
                                             " " +
                                             _calendarHelper.TimeSlotToTimeStamp(appointmentEntity.AppointmentData.TimeSlot) +
                                             " " + appointmentEntity.AppointmentData.Description + "\n";
                    }
                }
            }
            return appointmentsString;
        }

        /// <summary>
        /// Reads all existing entries from ASECalendarAppointments.json 
        /// </summary>
        /// <returns>
        /// A formatted string with appointment entries.
        /// </returns>
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
                var appointment = _customJsonConverter.DeserializeObject(subString);

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

        /// <summary>
        /// Reads all existing entries from ASECalendarAppointments.json for a selected month.
        /// </summary>
        /// <param name="selectedDate"></param>
        /// <returns>
        /// A dictionary where the keys are the days and the value is another dictionary with key 1-8 for time slots and appointments as value.
        /// </returns>
        public Dictionary<int, Dictionary<int, AppointmentEntity>> ReturnAllAppointmentsDictSelectedMonth(DateTime selectedDate)
        {
            var i = 0;
            Dictionary<int, Dictionary<int, AppointmentEntity>> appointmentDict = new();

            Dictionary<int, AppointmentEntity> appointmentEntities = new();

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
                            appointmentEntities[appointment.AppointmentData.TimeSlot] = appointment;
                            appointmentDict.Add(appointment.AppointmentData.Date.Day, appointmentEntities);
                        }

                        i++;
                    }
                }
            }

            return appointmentDict;
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

        public string ChangeDescription(Guid appointmentGuid, string newDescription)
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