namespace ASE_Calendar.ConsoleUI.Services
{
    /// <summary>
    ///     A service that contains helper functions for creating the calendar
    /// </summary>
    public class CalendarHelperService
    {
        /// <summary>
        ///     Converts a month number to a month name string.
        /// </summary>
        /// <param name="month"></param>
        /// <returns>
        ///     A string based on the input.
        /// </returns>
        public static string GetMonthDayString(int month)
        {
            var returnMonthString = month switch
            {
                1 => "Jan",
                2 => "Feb",
                3 => "Mar",
                4 => "Apr",
                5 => "Mai",
                6 => "Jun",
                7 => "Jul",
                8 => "Aug",
                9 => "Sep",
                10 => "Okt",
                11 => "Nov",
                12 => "Dez",
                _ => ""
            };
            return returnMonthString;
        }

        /// <summary>
        ///     Checks the number of days a given month in a given year has.
        /// </summary>
        /// <param name="month"></param>
        /// <param name="year"></param>
        /// <returns>
        ///     A integer.
        /// </returns>
        public static int GetMaxMonthDayInt(int month, int year)
        {
            var maxMonthDays = 0;
            const int thirtyone = 31;
            const int thirty = 30;

            var isLeapYear = GetLeapYear(year);

            switch (month)
            {
                case 1:
                    maxMonthDays = thirtyone;
                    break;
                case 2:
                    if (isLeapYear)
                        maxMonthDays = 29;
                    else
                        maxMonthDays = 28;
                    break;
                case 3:
                    maxMonthDays = thirtyone;
                    break;
                case 4:
                    maxMonthDays = thirty;
                    break;
                case 5:
                    maxMonthDays = thirtyone;
                    break;
                case 6:
                    maxMonthDays = thirty;
                    break;
                case 7:
                    maxMonthDays = thirtyone;
                    break;
                case 8:
                    maxMonthDays = thirtyone;
                    break;
                case 9:
                    maxMonthDays = thirty;
                    break;
                case 10:
                    maxMonthDays = thirtyone;
                    break;
                case 11:
                    maxMonthDays = thirty;
                    break;
                case 12:
                    maxMonthDays = thirtyone;
                    break;
            }

            return maxMonthDays;
        }

        /// <summary>
        ///     Checks whether a given year is a leap year or not.
        /// </summary>
        /// <param name="year"></param>
        /// <returns>
        ///     A boolean.
        /// </returns>
        private static bool GetLeapYear(int year)
        {
            if (year % 4 == 0 && year % 100 != 0 || year % 400 == 0) return true;

            return false;
        }

        /// <summary>
        ///     Converts a given time slot to a string containing the time
        /// </summary>
        /// <param name="timeSlot"></param>
        /// <returns>
        ///     A string.
        /// </returns>
        public string TimeSlotToTimeStamp(int timeSlot)
        {
            return timeSlot switch
            {
                1 => "08:00 - 09:00",
                2 => "09:00 - 10:00",
                3 => "10:00 - 11:00",
                4 => "11:00 - 12:00",
                5 => "13:00 - 14:00",
                6 => "14:00 - 15:00",
                7 => "15:00 - 16:00",
                8 => "16:00 - 17:00",
                _ => ""
            };
        }
    }
}