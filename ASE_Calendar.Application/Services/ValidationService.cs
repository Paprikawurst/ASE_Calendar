using ASE_Calendar.Domain.Aggregates;
using ASE_Calendar.Domain.Entities;

namespace ASE_Calendar.Application.Services
{
    /// <summary>
    ///  A service to manage the validation of entities.
    /// </summary>
    public class ValidationService
    {
        private readonly UserAggregate _userAggregate;
        private readonly AppointmentAggregate _appointmentAggregate;


        /// <summary>
        /// Validates user entities.
        /// </summary>
        /// <param name="userEntity"></param>
        public void ValidateUser(UserEntity userEntity)
        {
            _userAggregate.Validate(userEntity);
        }

        /// <summary>
        /// Validates appointment entities.
        /// </summary>
        /// <param name="appointmentEntity"></param>
        public void ValidateAppointment(AppointmentEntity appointmentEntity)
        {
            _appointmentAggregate.Validate(appointmentEntity);
        }
    }
}
