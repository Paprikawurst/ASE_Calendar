using System;

namespace ASE_Calendar.Classes
{
    public class CalendarHelper
    {
        public string GetMonthdayString(int month)
        {
            string returnMonthString = "";

            switch (month)
            {

                case 0:
                    returnMonthString = "Jan";
                    break;

                case 1:
                    returnMonthString = "Feb";
                    break;

                case 2:
                    returnMonthString = "Mar";
                    break;

                case 3:
                    returnMonthString = "Apr";
                    break;

                case 4:
                    returnMonthString = "Mai";
                    break;

                case 5:
                    returnMonthString = "Jun";
                    break;

                case 6:
                    returnMonthString = "Jul";
                    break;

                case 7:
                    returnMonthString = "Aug";
                    break;

                case 8:
                    returnMonthString = "Sep";
                    break;

                case 9:
                    returnMonthString = "Okt";
                    break;

                case 10:
                    returnMonthString = "Nov";
                    break;

                case 11:
                    returnMonthString = "Dez";
                    break;

            }
            return returnMonthString;
        }

        public int GetMaxMonthDayInt(int month, int year)
        {
            int maxMonthDays = 0;

            const int thirtyone = 31;
            const int thirty = 30;
            bool isLeapYear = GetLeapYear(year);

            switch (month)
            {

                case 0:
                    maxMonthDays = thirtyone;
                    break;

                case 1:
                    if (isLeapYear)
                    {
                        maxMonthDays = 29;
                    }
                    else
                    {
                        maxMonthDays = 28;
                    }
                    break;

                case 2:
                    maxMonthDays = thirtyone;
                    break;

                case 3:
                    maxMonthDays = thirty;
                    break;

                case 4:
                    maxMonthDays = thirtyone;
                    break;

                case 5:
                    maxMonthDays = thirty;
                    break;

                case 6:
                    maxMonthDays = thirtyone;
                    break;

                case 7:
                    maxMonthDays = thirtyone;
                    break;

                case 8:
                    maxMonthDays = thirty;
                    break;

                case 9:
                    maxMonthDays = thirtyone;
                    break;

                case 10:
                    maxMonthDays = thirty;
                    break;

                case 11:
                    maxMonthDays = thirtyone;
                    break;

            }
            return maxMonthDays;
        }

        private bool GetLeapYear(int year)
        {
            if (((year % 4 == 0) && (year % 100 != 0)) || (year % 400 == 0))
            {
                return true;
            }
            return false;
        }

        public void clear_screen()
        {
            int n;
            for (n = 0; n < 10; n++)
            {
                Console.WriteLine("\n\n\n\n\n\n\n\n\n\n");
            }
        }
    }
}