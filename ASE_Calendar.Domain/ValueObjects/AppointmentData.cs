using System;

namespace ASE_Calendar.Domain.ValueObjects
{
    public class AppointmentData
    {
        public AppointmentData(DateTime date, int timeSlot, string description)
        {
            Date = date;
            TimeSlot = timeSlot;
            Description = description;
        }

        public DateTime Date { get; init; }
        public int TimeSlot { get; init; }
        public string Description { get; init; }
    }
}