using System;

namespace ASE_Calendar.Domain.Entities
{
    public class AppointmentEntity
    {
        public ValueObjects.AppointmentData AppointmentData { get; init; }
        public ValueObjects.UserID UserId { get; init; }

        public AppointmentEntity(DateTime Date, ValueObjects.UserID UserId)
        {
            AppointmentData = new ValueObjects.AppointmentData(Date);
            this.UserId = UserId;
        }

    }
}