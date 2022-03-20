using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ASE_Calendar.Application.Services;
using ASE_Calendar.Domain.Entities;
using Newtonsoft.Json;

namespace ASE_Calendar.Application.Repositories
{
    public class AppointmentRepository
    {
        private UserEntity User { get; set; }
        private AppointmentEntity _appointment;
        private CalendarHelperService calendarHelper = new ();

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
                    var Appointment = JsonConvert.DeserializeObject<AppointmentEntity>(subString);

                    if (Appointment != null)
                    {
                        if (Appointment.UserId.Value == User.userId.Value)
                        {
                            appointmentsString = appointmentsString + Appointment.AppointmentData.Date.ToLongDateString() + " " +
                                                 calendarHelper.TimeSlotToTimeStamp(Appointment.AppointmentData.TimeSlot) + " " + Appointment.AppointmentData.Description + "\n";
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
                    var Appointment = JsonConvert.DeserializeObject<AppointmentEntity>(subString);

                    if (Appointment != null && Appointment.AppointmentData.Date.Month == selectedDate.Month && Appointment.AppointmentData.Date.Year == selectedDate.Year)
                    {
                        if (Appointment.UserId.Value == User.userId.Value)
                        {
                            appointmentDict.Add(Appointment.AppointmentData.Date.Day, Appointment);
                            i++;
                        }
                    }
                }
            }

            return appointmentDict;

        }

        private void CreateAppointment()
        {
            var json = JsonConvert.SerializeObject(_appointment);

            File.AppendAllText(AppDomain.CurrentDomain.BaseDirectory + "ASECalendarAppointments.json", json + "\n");

        }
    }
}
