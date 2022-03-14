using System;
using ASE_Calendar.Domain.ValueObjects;

namespace ASE_Calendar.Domain.Entities
{
    public class AppointmentEntity
    {
        public AppointmentData AppointmentData { get; init; }
        public UserID UserId { get; init; }

        public AppointmentEntity(DateTime Date, UserID UserId)
        {
            AppointmentData = new AppointmentData(Date);
            this.UserId = UserId;
        }

    }
}