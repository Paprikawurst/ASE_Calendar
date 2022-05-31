using ASE_Calendar.Domain.Entities;

namespace ASE_Calendar.Application.Repositories.AppointmentInterfaces
{
    public interface IAppointmentCreateRepository
    {
        void CreateAppointment(AppointmentEntity appointmentEntity);      
    }
}