using ASE_Calendar.Application.Repositories;
using ASE_Calendar.Domain.Entities;
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
            UserEntity userData = userRepository.ReturnUserEntity();

            //Assert
            Assert.AreEqual(userData.UserDataRegistered.Username, user.UserDataRegistered.Username);
            Assert.AreEqual(userData.UserDataRegistered.Password, user.UserDataRegistered.Password);
            Assert.AreEqual(userData.UserDataRegistered.RoleId, user.UserDataRegistered.RoleId);
            Assert.AreEqual(userData.UserId.Value, user.UserId.Value);
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