using ASE_Calendar.Application.Services;
using ASE_Calendar.Domain.Entities;
using ASE_Calendar.Application.Repositories;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.IO;
namespace ASE_Calendar.Tests
{
    [TestClass]
    public class UnitTestUserRepository
    {
        [TestMethod]
        public void CreateAndReturnUserTest()
        {
            // Arrange

            if (File.Exists(AppDomain.CurrentDomain.BaseDirectory + "ASECalendarUsers.json"))
            {
                File.Delete(AppDomain.CurrentDomain.BaseDirectory + "ASECalendarUsers.json");
            }

            UserEntity user = new UserEntity("Adrian", "12345", 0, Guid.NewGuid());
            UserRepository userRepository = new UserRepository(user);
            userRepository.Username = "Adrian";
            userRepository.Password = "12345";

            // Act
            UserEntity useerData = userRepository.ReturnUserEntity();

            //Assert
            Assert.AreEqual(useerData.UserDataRegistered.Username, user.UserDataRegistered.Username);
            Assert.AreEqual(useerData.UserDataRegistered.Password, user.UserDataRegistered.Password);
            Assert.AreEqual(useerData.UserDataRegistered.RoleId, user.UserDataRegistered.RoleId);
            Assert.AreEqual(useerData.UserId.Value, user.UserId.Value);
        }

        [TestMethod]
        public void ReturnTrueIfUsernameExistsTest()
        {
            // Arrange

            if (File.Exists(AppDomain.CurrentDomain.BaseDirectory + "ASECalendarUsers.json"))
            {
                File.Delete(AppDomain.CurrentDomain.BaseDirectory + "ASECalendarUsers.json");
            }

            UserEntity user = new UserEntity("Adrian", "12345", 0, Guid.NewGuid());
            UserRepository userRepository = new UserRepository(user);
            userRepository.Username = "Adrian";
            userRepository.Password = "12345";

            // Act
            bool exists = userRepository.ReadFromJsonFileReturnTrueIfUsernameExists();

            //Assert
            Assert.IsTrue(exists);
            
        }
    }
}