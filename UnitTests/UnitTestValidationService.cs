using ASE_Calendar.ConsoleUI.Services;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ASE_Calendar.Application.Services;
using ASE_Calendar.Domain.Entities;
using System;

namespace ASE_Calendar.Tests
{
    [TestClass]
    public class UnitTestValidationService
    {
        [TestMethod]
        public void NotEqualTest()
        {
            // Arrange
            ValidationService validationService = new ValidationService();

            DateTime dateTime = new DateTime(2022, 10, 15);
            int timeSlot = 5;

            UserEntity user = new UserEntity("Adrian", "12345", 0, Guid.NewGuid());
            AppointmentEntity appointmentEntity = new AppointmentEntity(dateTime, timeSlot, user.UserId, Guid.NewGuid(), "UnitTest");
       
            // Act
            validationService.ValidateAppointment(appointmentEntity);

            //Assert
            

        }

    }
}