using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using ASE_Calendar.Domain.Entities;

namespace ASE_Calendar.Domain.Aggregates
{
    /// <summary>
    /// An aggregate to validate the appointment entities with.
    /// </summary>
    public class AppointmentAggregate
    {
        private readonly List<ValidationResult> _validationResults = new List<ValidationResult>();

        /// <summary>
        /// Validates each value of the appointment entity.
        /// </summary>
        /// <param name="appointment"></param>
        public void Validate(AppointmentEntity appointment)
        {
            _validationResults.Clear();
            if (appointment.AppointmentId == null)
            {
                _validationResults.Add(new ValidationResult("Appointment ID is not set", new[] { nameof(appointment.AppointmentId) }));
            }

            if (appointment.UserId == null)
            {
                _validationResults.Add(new ValidationResult("Appointment has no User ID", new[] { nameof(appointment.AppointmentId) }));
            }

            if (string.IsNullOrWhiteSpace(appointment.AppointmentData.Description))
            {
                _validationResults.Add(new ValidationResult("Description is empty", new[] { nameof(appointment.AppointmentData.Description) }));
            }

            if (appointment.AppointmentData == null)
            {
                _validationResults.Add(new ValidationResult("Appointment data is null", new[] { nameof(appointment.AppointmentData)}));
            }

            if (appointment.AppointmentData.Date.Year <= DateTime.Now.Year)
            {
                _validationResults.Add(new ValidationResult("Year of date is incorrect", new[] { nameof(appointment.AppointmentData.Date) }));
            }

            if (!Enumerable.Range(1, 8).Contains(appointment.AppointmentData.TimeSlot))
            {
                _validationResults.Add(new ValidationResult("Incorrect TimeSlot", new[] { nameof(appointment.AppointmentData.TimeSlot) }));
            }

            if (_validationResults.Count > 0)
            {
                var appender = new StringBuilder();
                foreach (var validationResult in _validationResults)
                {
                    appender.Append(validationResult.ErrorMessage);
                }

                var validationErrors = appender.ToString();
                //TODO: Ausgabe von validationErrors über Log oder Exception
            }
        }
    }
}
