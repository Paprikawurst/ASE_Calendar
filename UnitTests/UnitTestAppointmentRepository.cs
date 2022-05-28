using ASE_Calendar.Domain.Entities;
using ASE_Calendar.Application.Repositories;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.IO;
using System;

namespace ASE_Calendar.Tests
{
    [TestClass]
    public class UnitTestAppointmentRepository
    {
        [TestMethod]
        public void CreateAndReturnAppointmentTest()
        {
            // Arrange
            if (File.Exists(AppDomain.CurrentDomain.BaseDirectory + "ASECalendarAppointments.json"))
            {
                File.Delete(AppDomain.CurrentDomain.BaseDirectory + "ASECalendarAppointments.json");
            }

            DateTime dateTime = new System.DateTime(2022, 10, 15);
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

        [TestMethod]
        public void ChangeAppointmentDateTest()
        {
            // Arrange
            DateTime dateTime = new DateTime(2022, 9, 15);
            DateTime testDateTime = new DateTime(2021, 9, 12);
            int timeSlot = 5;
            UserEntity user = new UserEntity("Nico", "12345", 0, Guid.NewGuid());
            AppointmentEntity appointmentEntity = new AppointmentEntity(dateTime, timeSlot, user.UserId, Guid.NewGuid(), "UnitTest");
            AppointmentRepository appointmentRepository = new AppointmentRepository();

            if (File.Exists(AppDomain.CurrentDomain.BaseDirectory + "ASECalendarAppointments.json"))
            {
                File.Delete(AppDomain.CurrentDomain.BaseDirectory + "ASECalendarAppointments.json");
            }

            // Act
            appointmentRepository.CreateAppointment(appointmentEntity);
            var appointmentDict = appointmentRepository.ReturnAllAppointmentsDict();
            appointmentRepository.ChangeDate(appointmentEntity.AppointmentId.Value, testDateTime);
            appointmentDict = appointmentRepository.ReturnAllAppointmentsDict();


            //Assert
            Assert.AreEqual(true, appointmentDict.ContainsKey(12));
            Assert.AreEqual(true, appointmentDict[12].ContainsKey(5));
            Assert.AreEqual(user.UserId.Value, appointmentDict[12][5].UserId.Value);
            Assert.AreEqual(appointmentEntity.AppointmentId.Value, appointmentDict[12][5].AppointmentId.Value);
            Assert.AreEqual(appointmentEntity.AppointmentData.TimeSlot, appointmentDict[12][5].AppointmentData.TimeSlot);
            Assert.AreEqual(testDateTime.Day, appointmentDict[12][5].AppointmentData.Date.Day);
            Assert.AreEqual(testDateTime.Month, appointmentDict[12][5].AppointmentData.Date.Month);
            Assert.AreEqual(testDateTime.Year, appointmentDict[12][5].AppointmentData.Date.Year);

        }

        [TestMethod]
        public void ChangeAppointmentDescriptionTest()
        {
            // Arrange
            DateTime dateTime = new DateTime(2022, 9, 15);
            int timeSlot = 5;
            string changedDescription = "UnitTestTest";
            UserEntity user = new UserEntity("Nico", "12345", 0, Guid.NewGuid());
            AppointmentEntity appointmentEntity = new AppointmentEntity(dateTime, timeSlot, user.UserId, Guid.NewGuid(), "UnitTest");
            AppointmentRepository appointmentRepository = new AppointmentRepository();

            if (File.Exists(AppDomain.CurrentDomain.BaseDirectory + "ASECalendarAppointments.json"))
            {
                File.Delete(AppDomain.CurrentDomain.BaseDirectory + "ASECalendarAppointments.json");
            }

            // Act
            appointmentRepository.CreateAppointment(appointmentEntity);
            var appointmentDict = appointmentRepository.ReturnAllAppointmentsDict();
            appointmentRepository.ChangeDescription(appointmentEntity.AppointmentId.Value, changedDescription);
            appointmentDict = appointmentRepository.ReturnAllAppointmentsDict();


            //Assert
            Assert.AreEqual(true, appointmentDict.ContainsKey(15));
            Assert.AreEqual(true, appointmentDict[15].ContainsKey(5));
            Assert.AreEqual(user.UserId.Value, appointmentDict[15][5].UserId.Value);
            Assert.AreEqual(appointmentEntity.AppointmentId.Value, appointmentDict[15][5].AppointmentId.Value);
            Assert.AreEqual(changedDescription, appointmentDict[15][5].AppointmentData.Description);

        }

        [TestMethod]
        public void DeleteAppointmentTest()
        {// Arrange
            DateTime dateTime = new DateTime(2022, 9, 15);
            int timeSlot = 5;
            string changedDescription = "UnitTestTest";
            UserEntity user = new UserEntity("Nico", "12345", 0, Guid.NewGuid());
            AppointmentEntity appointmentEntity = new AppointmentEntity(dateTime, timeSlot, user.UserId, Guid.NewGuid(), "UnitTest");
            AppointmentRepository appointmentRepository = new AppointmentRepository();

            if (File.Exists(AppDomain.CurrentDomain.BaseDirectory + "ASECalendarAppointments.json"))
            {
                File.Delete(AppDomain.CurrentDomain.BaseDirectory + "ASECalendarAppointments.json");
            }

            // Act
            appointmentRepository.CreateAppointment(appointmentEntity);
            var appointmentDict = appointmentRepository.ReturnAllAppointmentsDict();
            appointmentRepository.DeleteAppointment(appointmentEntity.AppointmentId.Value);
            appointmentDict = appointmentRepository.ReturnAllAppointmentsDict();


            //Assert
            Assert.IsFalse(appointmentDict.ContainsKey(9));
           
        }
    }
}