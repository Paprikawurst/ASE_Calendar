using ASE_Calendar.ConsoleUI.Services;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ASE_Calendar.Tests
{
    [TestClass]
    public class UnitTestCheckDateService
    {
        [TestMethod]
        public void NotEqualTest()
        {
            // Arrange
            int year = 2022;
            int month = 5;
            CheckDateService checkDateService = new CheckDateService(year, month);

            // Act
            var checkedDate = checkDateService.Check();

            //Assert
            Assert.AreEqual(2022, checkedDate.Year, "Year is not same");
            Assert.AreEqual(5, checkedDate.Month, "Month is not same");

        }

        [TestMethod]
        public void IncreaseTest()
        {
            // Arrange
            int year = 2022;
            int month3 = 13;

            CheckDateService checkDateService = new CheckDateService(year, month3);


            // Act
            var checkedDate = checkDateService.Check();

            //Assert
            Assert.AreEqual(2023, checkedDate.Year, "Year did not increase");
            Assert.AreEqual(1, checkedDate.Month, "Month did not increase");
        }

        [TestMethod]
        public void DecreaseTest()
        {
            // Arrange
            int year = 2022;
            int month2 = 0;

            CheckDateService checkDateService = new CheckDateService(year, month2);

            // Act
            var checkedDate = checkDateService.Check();

            //Assert
            Assert.AreEqual(2021, checkedDate.Year, "Year did not decrease");
            Assert.AreEqual(12, checkedDate.Month, "Month did not decrease");
        }
    }
}
