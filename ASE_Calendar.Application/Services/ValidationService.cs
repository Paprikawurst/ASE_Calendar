using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ASE_Calendar.Domain.Aggregates;
using ASE_Calendar.Domain.Entities;

namespace ASE_Calendar.Application.Services
{
    public class ValidationService
    {
        private readonly UserAggregate _userAggregate;
        private readonly AppointmentAggregate _appointmentAggregate;



        public void ValidateUser(UserEntity userEntity)
        {
            _userAggregate.Validate(userEntity);
        }

        public void ValidateAppointment(AppointmentEntity appointmentEntity)
        {
            _appointmentAggregate.Validate(appointmentEntity);
        }
    }
}
