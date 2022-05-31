using ASE_Calendar.Application.Repositories;
using ASE_Calendar.Domain.Entities;
using FakeItEasy;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.IO;

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

            UserEntity fakeUser = A.Fake<UserEntity>(x => x.WithArgumentsForConstructor(() => new UserEntity("Adrian", "12345", 0, Guid.NewGuid())));
            AppointmentEntity fakeAppointment = A.Fake<AppointmentEntity>(x => x.WithArgumentsForConstructor(() => new AppointmentEntity(
                DateTime.Now, 4, fakeUser.UserId, Guid.NewGuid(), "iAmATest")));
            AppointmentRepository appointmentRepository = new();

            // Act
            appointmentRepository.CreateAppointment(fakeAppointment);
            var appointmentDict = appointmentRepository.ReturnAllAppointmentsDict();

            //Assert
            Assert.AreEqual(true, appointmentDict.ContainsKey(fakeAppointment.AppointmentData.Date.Day));
            Assert.AreEqual(true, appointmentDict[fakeAppointment.AppointmentData.Date.Day].ContainsKey(fakeAppointment.AppointmentData.TimeSlot));
            Assert.AreEqual(fakeUser.UserId.Value, appointmentDict[fakeAppointment.AppointmentData.Date.Day][fakeAppointment.AppointmentData.TimeSlot].UserId.Value);
            Assert.AreEqual(fakeAppointment.AppointmentId.Value, appointmentDict[fakeAppointment.AppointmentData.Date.Day][fakeAppointment.AppointmentData.TimeSlot].AppointmentId.Value);
            Assert.AreEqual(fakeAppointment.AppointmentData.TimeSlot, appointmentDict[fakeAppointment.AppointmentData.Date.Day][fakeAppointment.AppointmentData.TimeSlot].AppointmentData.TimeSlot);
            Assert.AreEqual(fakeAppointment.AppointmentData.Description, appointmentDict[fakeAppointment.AppointmentData.Date.Day][fakeAppointment.AppointmentData.TimeSlot].AppointmentData.Description);
            Assert.AreEqual(fakeAppointment.AppointmentData.Date.Day, appointmentDict[fakeAppointment.AppointmentData.Date.Day][fakeAppointment.AppointmentData.TimeSlot].AppointmentData.Date.Day);
            Assert.AreEqual(fakeAppointment.AppointmentData.Date.Month, appointmentDict[fakeAppointment.AppointmentData.Date.Day][fakeAppointment.AppointmentData.TimeSlot].AppointmentData.Date.Month);
            Assert.AreEqual(fakeAppointment.AppointmentData.Date.Year, appointmentDict[fakeAppointment.AppointmentData.Date.Day][fakeAppointment.AppointmentData.TimeSlot].AppointmentData.Date.Year);

        }

        [TestMethod]
        public void ChangeAppointmentDateTest()
        {
            // Arrange
            DateTime testDateTime = new(2021, 9, 12);
            UserEntity fakeUser = A.Fake<UserEntity>(x => x.WithArgumentsForConstructor(() => new UserEntity("Adrian", "12345", 0, Guid.NewGuid())));
            AppointmentEntity fakeAppointment = A.Fake<AppointmentEntity>(x => x.WithArgumentsForConstructor(() => new AppointmentEntity(
                DateTime.Now, 4, fakeUser.UserId, Guid.NewGuid(), "iAmATest")));
            AppointmentRepository appointmentRepository = new();

            if (File.Exists(AppDomain.CurrentDomain.BaseDirectory + "ASECalendarAppointments.json"))
            {
                File.Delete(AppDomain.CurrentDomain.BaseDirectory + "ASECalendarAppointments.json");
            }

            // Act
            appointmentRepository.CreateAppointment(fakeAppointment);
            var appointmentDict = appointmentRepository.ReturnAllAppointmentsDict();
            appointmentRepository.ChangeDate(fakeAppointment.AppointmentId.Value, testDateTime);
            appointmentDict = appointmentRepository.ReturnAllAppointmentsDict();


            //Assert
            Assert.AreEqual(true, appointmentDict.ContainsKey(testDateTime.Day));
            Assert.AreEqual(true, appointmentDict[testDateTime.Day].ContainsKey(fakeAppointment.AppointmentData.TimeSlot));
            Assert.AreEqual(fakeUser.UserId.Value, appointmentDict[testDateTime.Day][fakeAppointment.AppointmentData.TimeSlot].UserId.Value);
            Assert.AreEqual(fakeAppointment.AppointmentId.Value, appointmentDict[testDateTime.Day][fakeAppointment.AppointmentData.TimeSlot].AppointmentId.Value);
            Assert.AreEqual(fakeAppointment.AppointmentData.TimeSlot, appointmentDict[testDateTime.Day][fakeAppointment.AppointmentData.TimeSlot].AppointmentData.TimeSlot);
            Assert.AreEqual(testDateTime.Day, appointmentDict[testDateTime.Day][fakeAppointment.AppointmentData.TimeSlot].AppointmentData.Date.Day);
            Assert.AreEqual(testDateTime.Month, appointmentDict[testDateTime.Day][fakeAppointment.AppointmentData.TimeSlot].AppointmentData.Date.Month);
            Assert.AreEqual(testDateTime.Year, appointmentDict[testDateTime.Day][fakeAppointment.AppointmentData.TimeSlot].AppointmentData.Date.Year);

        }

        [TestMethod]
        public void ChangeAppointmentDescriptionTest()
        {
            // Arrange
            string changedDescription = "UnitTestTest";
            UserEntity fakeUser = A.Fake<UserEntity>(x => x.WithArgumentsForConstructor(() => new UserEntity("Adrian", "12345", 0, Guid.NewGuid())));
            AppointmentEntity fakeAppointment = A.Fake<AppointmentEntity>(x => x.WithArgumentsForConstructor(() => new AppointmentEntity(
                DateTime.Now, 4, fakeUser.UserId, Guid.NewGuid(), "iAmATest")));
            AppointmentRepository appointmentRepository = new();

            if (File.Exists(AppDomain.CurrentDomain.BaseDirectory + "ASECalendarAppointments.json"))
            {
                File.Delete(AppDomain.CurrentDomain.BaseDirectory + "ASECalendarAppointments.json");
            }

            // Act
            appointmentRepository.CreateAppointment(fakeAppointment);
            var appointmentDict = appointmentRepository.ReturnAllAppointmentsDict();
            appointmentRepository.ChangeDescription(fakeAppointment.AppointmentId.Value, changedDescription);
            appointmentDict = appointmentRepository.ReturnAllAppointmentsDict();


            //Assert
            Assert.AreEqual(true, appointmentDict.ContainsKey(fakeAppointment.AppointmentData.Date.Day));
            Assert.AreEqual(true, appointmentDict[fakeAppointment.AppointmentData.Date.Day].ContainsKey(fakeAppointment.AppointmentData.TimeSlot));
            Assert.AreEqual(fakeUser.UserId.Value, appointmentDict[fakeAppointment.AppointmentData.Date.Day][fakeAppointment.AppointmentData.TimeSlot].UserId.Value);
            Assert.AreEqual(fakeAppointment.AppointmentId.Value, appointmentDict[fakeAppointment.AppointmentData.Date.Day][fakeAppointment.AppointmentData.TimeSlot].AppointmentId.Value);
            Assert.AreEqual(changedDescription, appointmentDict[fakeAppointment.AppointmentData.Date.Day][fakeAppointment.AppointmentData.TimeSlot].AppointmentData.Description);

        }

        [TestMethod]
        public void DeleteAppointmentTest()
        {
            // Arrange
            UserEntity fakeUser = A.Fake<UserEntity>(x => x.WithArgumentsForConstructor(() => new UserEntity("Adrian", "12345", 0, Guid.NewGuid())));
            AppointmentEntity fakeAppointment = A.Fake<AppointmentEntity>(x => x.WithArgumentsForConstructor(() => new AppointmentEntity(
                DateTime.Now, 4, fakeUser.UserId, Guid.NewGuid(), "iAmATest")));
            AppointmentRepository appointmentRepository = new();

            if (File.Exists(AppDomain.CurrentDomain.BaseDirectory + "ASECalendarAppointments.json"))
            {
                File.Delete(AppDomain.CurrentDomain.BaseDirectory + "ASECalendarAppointments.json");
            }

            // Act
            appointmentRepository.CreateAppointment(fakeAppointment);
            var appointmentDict = appointmentRepository.ReturnAllAppointmentsDict();
            appointmentRepository.DeleteAppointment(fakeAppointment.AppointmentId.Value);
            appointmentDict = appointmentRepository.ReturnAllAppointmentsDict();


            //Assert
            Assert.IsFalse(appointmentDict.ContainsKey(fakeAppointment.AppointmentData.Date.Day));

        }
    }
}