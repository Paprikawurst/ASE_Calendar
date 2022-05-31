using ASE_Calendar.Domain.ValueObjects;

namespace ASE_Calendar.Domain.Entities
{
    public interface IAppointmentEntity
    {
        AppointmentData AppointmentData { get; init; }
        AppointmentId AppointmentId { get; init; }
        UserId UserId { get; init; }
    }
}