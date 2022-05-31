using ASE_Calendar.Application.Services;
using ASE_Calendar.Domain.Entities;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.IO;

namespace ASE_Calendar.Tests
{
    [TestClass]
    public class UnitTestValidationService
    {
        [TestMethod]
        public void AppointmentAggregateTest()
        {
            // Arrange
            ValidationService validationService = new ValidationService();

            DateTime dateTime = new DateTime(2022, 10, 15);
            int timeSlot = 5;

            UserEntity user = new UserEntity("Adrian", "12345", 0, Guid.NewGuid());
            AppointmentEntity appointmentEntity = new AppointmentEntity(dateTime, timeSlot, user.UserId, Guid.NewGuid(), "UnitTest");

            // Act
            if (File.Exists(AppDomain.CurrentDomain.BaseDirectory + "ASECalendarLog.txt"))
            {
                File.Delete(AppDomain.CurrentDomain.BaseDirectory + "ASECalendarLog.txt");
            }

            validationService.ValidateAppointment(appointmentEntity);

            //Assert
            Assert.IsFalse(File.Exists(AppDomain.CurrentDomain.BaseDirectory + "ASECalendarLog.txt"));

        }

        [TestMethod]
        public void UserAggregateTest()
        {
            // Arrange
            ValidationService validationService = new ValidationService();
            UserEntity user = new UserEntity("Adrian", "12345", 0, Guid.NewGuid());

            // Act
            if (File.Exists(AppDomain.CurrentDomain.BaseDirectory + "ASECalendarLog.txt"))
            {
                File.Delete(AppDomain.CurrentDomain.BaseDirectory + "ASECalendarLog.txt");
            }
            validationService.ValidateUser(user);

            //Assert
            Assert.IsFalse(File.Exists(AppDomain.CurrentDomain.BaseDirectory + "ASECalendarLog.txt"));

        }

    }
}