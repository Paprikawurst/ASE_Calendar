using System;

namespace ASE_Calendar.ConsoleUI.Services
{
    /// <summary>
    ///     A service that checks whether a given month and year is input correctly.
    /// </summary>
    public class CheckDateService
    {
        private int _month;
        private int _year;

        public CheckDateService(int year, int month)
        {
            _year = year;
            _month = month;
        }

        /// <summary>
        ///     Checks whether previously set month and year can be converted into a DateTime object.
        /// </summary>
        /// <returns>
        ///     A DateTime object.
        /// </returns>
        public DateTime AdjustYearAndMonthReturnDateTime()
        {
            if (_month > 12)
            {
                _year += 1;
                _month = 1;
            }

            if (_month <= 0)
            {
                _year -= 1;
                _month = 12;
            }

            return new DateTime(_year, _month, 1);
        }
    }
}