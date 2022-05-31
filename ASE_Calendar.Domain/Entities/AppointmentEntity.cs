using System;
using ASE_Calendar.Domain.ValueObjects;

namespace ASE_Calendar.Domain.Entities
{
    /// <summary>
    ///     An entity that contains appointment information.
    /// </summary>
    public class AppointmentEntity : IAppointmentEntity
    {
        public AppointmentEntity(DateTime date, int timeSlot, UserId userId, Guid appointmentIdGuid, string description)
        {
            AppointmentData = new AppointmentData(date, timeSlot, description);
            UserId = userId;
            AppointmentId = new AppointmentId(appointmentIdGuid);
        }

        public AppointmentData AppointmentData { get; init; }
        public UserId UserId { get; init; }
        public AppointmentId AppointmentId { get; init; }
    }
}