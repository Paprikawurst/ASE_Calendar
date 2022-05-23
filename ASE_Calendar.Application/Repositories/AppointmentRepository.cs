using System;
using System.Collections.Generic;
using System.IO;
using ASE_Calendar.Application.Services;
using ASE_Calendar.Application.Shared;
using ASE_Calendar.Domain.Entities;

namespace ASE_Calendar.Application.Repositories
{
    /// <summary>
    ///     This repository manages the CRUD operations for the appointment entity.
    /// </summary>
    public class AppointmentRepository
    {
        private readonly CalendarHelperService _calendarHelper = new();
        private readonly CustomJsonConverter<AppointmentEntity> _customJsonConverter = new();

        /// <summary>
        ///     Serializes an appointment entity to a json format and appends it to the ASECalendarAppointments.json
        /// </summary>
        /// <param name="appointmentEntity"></param>
        public void CreateAppointment(AppointmentEntity appointmentEntity)
        {
            var json = _customJsonConverter.SerializeObject(appointmentEntity);
            File.AppendAllText(AppDomain.CurrentDomain.BaseDirectory + "ASECalendarAppointments.json", json + "\n");
        }

        public Dictionary<int, Dictionary<int, AppointmentEntity>> ReturnAllAppointmentsDict()
        {
            Dictionary<int, Dictionary<int, AppointmentEntity>> appointmentDict = new();

            if (File.Exists(AppDomain.CurrentDomain.BaseDirectory + "ASECalendarAppointments.json"))
            {
                var json = File.ReadAllText(AppDomain.CurrentDomain.BaseDirectory + "ASECalendarAppointments.json");
                var jsonSplit = json.Split("\n");

                foreach (var subString in jsonSplit)
                {
                    var appointmentEntity = _customJsonConverter.DeserializeObject(subString);

                    if (appointmentEntity != null)
                    {
                        if (appointmentDict.ContainsKey(appointmentEntity.AppointmentData.Date.Day))
                        {
                            appointmentDict[appointmentEntity.AppointmentData.Date.Day][
                                appointmentEntity.AppointmentData.TimeSlot] = appointmentEntity;
                        }
                        else
                        {
                            Dictionary<int, AppointmentEntity> appointmentEntities = new();
                            appointmentEntities[appointmentEntity.AppointmentData.TimeSlot] = appointmentEntity;
                            appointmentDict.Add(appointmentEntity.AppointmentData.Date.Day, appointmentEntities);
                        }
                    }
                }
            }

            return appointmentDict;
        }

        /// <summary>
        ///     Reads all existing entries from ASECalendarAppointments.json for a selected month.
        /// </summary>
        /// <param name="selectedDate"></param>
        /// <returns>
        ///     A dictionary where the keys are the days and the value is another dictionary with key 1-8 for time slots and
        ///     appointments as value.
        /// </returns>
        public Dictionary<int, Dictionary<int, AppointmentEntity>> ReturnAllAppointmentsDictSelectedMonth(
            DateTime selectedDate)
        {
            Dictionary<int, Dictionary<int, AppointmentEntity>> appointmentDict = new();

            if (File.Exists(AppDomain.CurrentDomain.BaseDirectory + "ASECalendarAppointments.json"))
            {
                var json = File.ReadAllText(AppDomain.CurrentDomain.BaseDirectory + "ASECalendarAppointments.json");
                var jsonSplit = json.Split("\n");

                foreach (var subString in jsonSplit)
                {
                    var appointmentEntity = _customJsonConverter.DeserializeObject(subString);

                    if (appointmentEntity != null &&
                        appointmentEntity.AppointmentData.Date.Month == selectedDate.Month &&
                        appointmentEntity.AppointmentData.Date.Year == selectedDate.Year)
                    {
                        if (appointmentDict.ContainsKey(appointmentEntity.AppointmentData.Date.Day))
                        {
                            appointmentDict[appointmentEntity.AppointmentData.Date.Day][
                                appointmentEntity.AppointmentData.TimeSlot] = appointmentEntity;
                        }
                        else
                        {
                            Dictionary<int, AppointmentEntity> appointmentEntities = new();
                            appointmentEntities[appointmentEntity.AppointmentData.TimeSlot] = appointmentEntity;
                            appointmentDict.Add(appointmentEntity.AppointmentData.Date.Day, appointmentEntities);
                        }
                    }
                }
            }

            return appointmentDict;
        }

        /// <summary>
        ///     Removes an existing appointment via a given Guid.
        /// </summary>
        /// <param name="appointmentGuid"></param>
        /// <returns>
        ///     A bool which indicates whether the appointment was deleted or not.
        /// </returns>
        public bool DeleteAppointment(Guid appointmentGuid)
        {
            if (!File.Exists(AppDomain.CurrentDomain.BaseDirectory + "ASECalendarAppointments.json"))
            {
                return false;
            }

            var json = File.ReadAllText(AppDomain.CurrentDomain.BaseDirectory + "ASECalendarAppointments.json");
            var jsonSplit = json.Split("\n");
            var i = 0;

            foreach (var subString in jsonSplit)
            {
                var appointmentEntity = _customJsonConverter.DeserializeObject(subString);

                if (appointmentEntity != null)
                {
                    if (appointmentEntity.AppointmentId.Value == appointmentGuid)
                    {
                        jsonSplit[i] = "";
                    }
                }

                i++;
            }

            RecreateFile(jsonSplit);
            return true;
        }

        /// <summary>
        ///     Change description of appointment via Guid and given new description.
        /// </summary>
        /// <param name="appointmentGuid"></param>
        /// <param name="newDescription"></param>
        /// <returns>
        ///     A bool which indicates whether the appointment description was successfully changed or not.
        /// </returns>
        public bool ChangeDescription(Guid appointmentGuid, string newDescription)
        {
            if (!File.Exists(AppDomain.CurrentDomain.BaseDirectory + "ASECalendarAppointments.json"))
            {
                return false;
            }

            var json = File.ReadAllText(AppDomain.CurrentDomain.BaseDirectory + "ASECalendarAppointments.json");
            var jsonSplit = json.Split("\n");
            AppointmentEntity changedAppointment = null;
            var i = 0;

            foreach (var subString in jsonSplit)
            {
                var appointmentEntity = _customJsonConverter.DeserializeObject(subString);

                if (appointmentEntity != null)
                {
                    if (appointmentEntity.AppointmentId.Value == appointmentGuid)
                    {
                        jsonSplit[i] = "";
                        changedAppointment = new AppointmentEntity(appointmentEntity.AppointmentData.Date,
                            appointmentEntity.AppointmentData.TimeSlot, appointmentEntity.UserId,
                            appointmentEntity.AppointmentId.Value,
                            newDescription);
                    }
                }

                i++;
            }

            RecreateFile(jsonSplit);
            return true;
        }

        /// <summary>
        ///     Change date of appointment via Guid and given new date.
        /// </summary>
        /// <param name="appointmentGuid"></param>
        /// <param name="newDate"></param>
        /// <returns>
        ///     A bool which indicates whether the appointment date was successfully changed or not.
        /// </returns>
        public bool ChangeDate(Guid appointmentGuid, DateTime newDate)
        {
            if (!File.Exists(AppDomain.CurrentDomain.BaseDirectory + "ASECalendarAppointments.json"))
            {
                return false;
            }

            var json = File.ReadAllText(AppDomain.CurrentDomain.BaseDirectory + "ASECalendarAppointments.json");
            var jsonSplit = json.Split("\n");
            AppointmentEntity changedAppointment = null;
            var i = 0;

            foreach (var subString in jsonSplit)
            {
                var appointmentEntity = _customJsonConverter.DeserializeObject(subString);

                if (appointmentEntity != null)
                {
                    if (appointmentEntity.AppointmentId.Value == appointmentGuid)
                    {
                        jsonSplit[i] = "";
                        changedAppointment = new AppointmentEntity(newDate,
                            appointmentEntity.AppointmentData.TimeSlot, appointmentEntity.UserId,
                            appointmentEntity.AppointmentId.Value,
                            appointmentEntity.AppointmentData.Description);
                    }
                }

                i++;
            }

            RecreateFile(jsonSplit);
            CreateAppointment(changedAppointment);
            return true;
        }

        /// <summary>
        ///     Deletes ASECalendarAppointments.json and recreates it with given json-Objects
        /// </summary>
        /// <param name="jsonSplit"></param>
        private void RecreateFile(string[] jsonSplit)
        {
            File.Delete(AppDomain.CurrentDomain.BaseDirectory + "ASECalendarAppointments.json");

            foreach (var subString in jsonSplit)
            {
                if (subString != "")
                {
                    var appointmentEntity = _customJsonConverter.DeserializeObject(subString);
                    CreateAppointment(appointmentEntity);
                }
            }
        }
    }
}