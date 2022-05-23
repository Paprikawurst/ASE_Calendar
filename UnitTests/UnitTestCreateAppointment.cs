using ASE_Calendar.Application.Services;
using ASE_Calendar.Domain.Entities;
using ASE_Calendar.Application.Repositories;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace ASE_Calendar.Tests
{
    [TestClass]
    public class UnitTestCreateAppointment
    {
        [TestMethod]
        public void CreateAppointmentTest()
        {
            // Arrange
            System.DateTime dateTime = new System.DateTime(2022, 10, 15);
            int timeSlot = 5;
            UserEntity user = new UserEntity("Adrian", "12345", 0, Guid.NewGuid());
            AppointmentEntity appointmentEntity = new AppointmentEntity(dateTime, timeSlot, user.UserId, Guid.NewGuid(), "UnitTest");
            AppointmentRepository appointmentRepository = new AppointmentRepository();
            
      
            // Act
            appointmentRepository.CreateAppointment(appointmentEntity);
            var appointmentDict = appointmentRepository.ReturnAllAppointmentsDict();

            //Assert
            Assert.AreEqual(true, appointmentDict.ContainsKey(15));
            Assert.AreEqual(true, appointmentDict[15].ContainsKey(5));
            Assert.AreEqual(user.UserId.Value, appointmentDict[15][5].UserId.Value);
            Assert.AreEqual(appointmentEntity.AppointmentId.Value, appointmentDict[15][5].AppointmentId.Value);
            Assert.AreEqual(appointmentEntity.AppointmentData.TimeSlot, appointmentDict[15][5].AppointmentData.TimeSlot);
            Assert.AreEqual(appointmentEntity.AppointmentData.Description, appointmentDict[15][5].AppointmentData.Description);
            Assert.AreEqual(appointmentEntity.AppointmentData.Date.Day, appointmentDict[15][5].AppointmentData.Date.Day);
            Assert.AreEqual(appointmentEntity.AppointmentData.Date.Month, appointmentDict[15][5].AppointmentData.Date.Month);
            Assert.AreEqual(appointmentEntity.AppointmentData.Date.Year, appointmentDict[15][5].AppointmentData.Date.Year);

        }
    }
}