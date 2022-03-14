using System;
using ASE_Calendar.Domain.ValueObjects;

namespace ASE_Calendar.Domain.Entities
{
    public class AppointmentEntity
    {
        public AppointmentData AppointmentData { get; init; }
        public UserID UserId { get; init; }

        public AppointmentEntity(DateTime Date,int timeSlot, UserID UserId)
        {
            AppointmentData = new AppointmentData(Date, timeSlot);
            this.UserId = UserId;
        }

    }
}