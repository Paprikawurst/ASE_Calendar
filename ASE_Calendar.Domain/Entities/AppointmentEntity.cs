using System;

namespace ASE_Calendar.Domain.Entities
{
    public class AppointmentEntity
    {
        private ValueObjects.AppointmentData AppointmentData;
        private ValueObjects.UserID UserId;

        public AppointmentEntity(DateTime Date, ValueObjects.UserID UserId)
        {
            AppointmentData = new ValueObjects.AppointmentData(Date);
            this.UserId = UserId;
        }

    }
}