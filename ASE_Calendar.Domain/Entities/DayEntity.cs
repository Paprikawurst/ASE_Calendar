using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ASE_Calendar.Domain.ValueObjects;

namespace ASE_Calendar.Domain.Entities
{
    public class DayEntity
    {
        public DateTime Date { get; set; }
        public DayId DayId { get; init; }
        public AppointmentEntity TimeSlot1 { get; set; }
        public AppointmentEntity TimeSlot2 { get; set; }
        public AppointmentEntity TimeSlot3 { get; set; }
        public AppointmentEntity TimeSlot4 { get; set; }
        public AppointmentEntity TimeSlot5 { get; set; }
        public AppointmentEntity TimeSlot6 { get; set; }
        public AppointmentEntity TimeSlot7 { get; set; }
        public AppointmentEntity TimeSlot8 { get; set; }

        public DayEntity(Guid dayIdGuid, DateTime date)
        {
            Date = date;
            DayId = new DayId(dayIdGuid);
            TimeSlot1 = null;
            TimeSlot2 = null;
            TimeSlot3 = null;
            TimeSlot4 = null;
            TimeSlot5 = null;
            TimeSlot6 = null;
            TimeSlot7 = null;
            TimeSlot8 = null;
        }
    }
}
