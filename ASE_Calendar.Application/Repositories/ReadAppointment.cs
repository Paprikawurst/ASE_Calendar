using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ASE_Calendar.Application.Repositories
{
    class ReadAppointment
    {
        public Domain.Entities.UserEntity user { get; set; }

        public ReadAppointment(Domain.Entities.UserEntity User)
        {
            this.user = user;

        }

        public Domain.Entities.AppointmentEntity ReadFromJsonFile()
        {
            string json = File.ReadAllText(AppDomain.CurrentDomain.BaseDirectory + "ASECalendarAppointments.json");
            string[] jsonSplit = json.Split("\n");

            foreach (var subString in jsonSplit)
            {
                var Appointment = JsonConvert.DeserializeObject<Domain.Entities.AppointmentEntity>(subString);

                if (Appointment != null)
                {
                    if (Appointment.UserId == user.userId)
                    {
                        return Appointment;
                    }
                }
            }

            return null;

        }
    }
}
