using ASE_Calendar.ConsoleUI.Services;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ASE_Calendar.Tests
{
    [TestClass]
    public class UnitTestCheckDateService
    {
        [TestMethod]
        public void TestCheck()
        {
            // Arrange
            int year = 2022;
            int month = 5;
            int month2 = 0;
            int month3 = 13;

            CheckDateService checkDateService1 = new CheckDateService(year, month);
            CheckDateService checkDateService2 = new CheckDateService(year, month2);
            CheckDateService checkDateService3 = new CheckDateService(year, month3);


            // Act
            var test1 = checkDateService1.Check();
            var test2 = checkDateService2.Check();
            var test3 = checkDateService3.Check();

            //Assert
            Assert.AreEqual(2022,test1.Year, "Year is not same");
            Assert.AreEqual(5, test1.Month, "Month is not same");

            Assert.AreEqual(2021, test2.Year, "Year did not decrease");
            Assert.AreEqual(12, test2.Month, "Month did not decrease");

            Assert.AreEqual(2023, test3.Year, "Year did not increase");
            Assert.AreEqual(1, test3.Month, "Month did not increase");
        }
    }
}
