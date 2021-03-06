using ASE_Calendar.Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Linq;

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
                _validationResults.Add(new ValidationResult("Appointment data is null", new[] { nameof(appointment.AppointmentData) }));
            }

            if (!Enumerable.Range(1, 8).Contains(appointment.AppointmentData.TimeSlot))
            {
                _validationResults.Add(new ValidationResult("Incorrect TimeSlot", new[] { nameof(appointment.AppointmentData.TimeSlot) }));
            }

            if (_validationResults.Count > 0)
            {
                var errorTime = DateTime.Now;

                if (!File.Exists(AppDomain.CurrentDomain.BaseDirectory + "ASECalendarLog.txt"))
                {
                    var fileStream = File.Create(AppDomain.CurrentDomain.BaseDirectory + "ASECalendarLog.txt", 40000);
                    fileStream.Close();
                }

                foreach (var validationResult in _validationResults)
                {
                    File.AppendAllText(AppDomain.CurrentDomain.BaseDirectory + "ASECalendarLog.txt", "Appointment Error: " + validationResult + " " + errorTime.ToString() + "\n");
                }
            }
        }
    }
}
