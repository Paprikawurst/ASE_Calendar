using ASE_Calendar.Application.Services;
using ASE_Calendar.Domain.Entities;
using ASE_Calendar.Application.Repositories;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
namespace ASE_Calendar.Tests
{
    [TestClass]
    public class UnitTestChangeAppointment
    {
        [TestMethod]
        public void ChangeAppointmentTest()
        {// Arrange
            DateTime dateTime = new DateTime(2022, 9, 15);
            DateTime testDateTime = new DateTime(2021, 9, 12);
            int timeSlot = 5;
            string changedDescription = "UnitTestTest";
            UserEntity user = new UserEntity("Nico", "12345", 0, Guid.NewGuid());
            AppointmentEntity appointmentEntity = new AppointmentEntity(dateTime, timeSlot, user.UserId, Guid.NewGuid(), "UnitTest");
            AppointmentRepository appointmentRepository = new AppointmentRepository();


            // Act
            appointmentRepository.CreateAppointment(appointmentEntity);
            var appointmentDict = appointmentRepository.ReturnAllAppointmentsDict();
            appointmentRepository.ChangeDate(appointmentEntity.AppointmentId.Value, testDateTime);
            appointmentRepository.ChangeDescription(appointmentEntity.AppointmentId.Value, changedDescription);
            appointmentDict = appointmentRepository.ReturnAllAppointmentsDict();


            //Assert
            Assert.AreEqual(true, appointmentDict.ContainsKey(12));
            Assert.AreEqual(true, appointmentDict[12].ContainsKey(5));
            Assert.AreEqual(user.UserId.Value, appointmentDict[12][5].UserId.Value);
            Assert.AreEqual(appointmentEntity.AppointmentId.Value, appointmentDict[12][5].AppointmentId.Value);
            Assert.AreEqual(appointmentEntity.AppointmentData.TimeSlot, appointmentDict[12][5].AppointmentData.TimeSlot);
            Assert.AreEqual(changedDescription, appointmentDict[12][5].AppointmentData.Description);
            Assert.AreEqual(testDateTime.Day, appointmentDict[12][5].AppointmentData.Date.Day);
            Assert.AreEqual(testDateTime.Month, appointmentDict[12][5].AppointmentData.Date.Month);
            Assert.AreEqual(testDateTime.Year, appointmentDict[12][5].AppointmentData.Date.Year);

        }
    }
}