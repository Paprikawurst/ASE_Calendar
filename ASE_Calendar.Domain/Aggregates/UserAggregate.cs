using ASE_Calendar.Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Linq;

namespace ASE_Calendar.Domain.Aggregates
{
    /// <summary>
    /// An aggregate to validate user entities with.
    /// </summary>
    public class UserAggregate
    {
        private readonly List<ValidationResult> _validationResults = new List<ValidationResult>();

        /// <summary>
        /// Validates each value of the user entity.
        /// </summary>
        /// <param name="user"></param>
        public void Validate(UserEntity user)
        {
            _validationResults.Clear();
            if (user.UserId == null)
            {
                _validationResults.Add(new ValidationResult("User ID is not set", new[] { nameof(user.UserId) }));
            }

            if (string.IsNullOrWhiteSpace(user.UserDataRegistered.Username))
            {
                _validationResults.Add(new ValidationResult("Username is empty", new[] { nameof(user.UserDataRegistered.Username) }));
            }

            if (user.UserDataRegistered == null)
            {
                _validationResults.Add(new ValidationResult("User data is null", new[] { nameof(user.UserDataRegistered) }));
            }

            if (string.IsNullOrWhiteSpace(user.UserDataRegistered.Password))
            {
                _validationResults.Add(new ValidationResult("Password is empty", new[] { nameof(user.UserDataRegistered.Password) }));
            }

            if (!Enumerable.Range(0, 3).Contains(user.UserDataRegistered.RoleId))
            {
                _validationResults.Add(new ValidationResult("User role ID is not in range", new[] { nameof(user.UserDataRegistered.RoleId) }));
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
                    File.AppendAllText(AppDomain.CurrentDomain.BaseDirectory + "ASECalendarLog.txt", "User Error: " + validationResult + " " + errorTime.ToString() + "\n");
                }
            }
        }
    }
}
