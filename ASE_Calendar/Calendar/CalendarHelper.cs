using System;
using System.Collections.Generic;

namespace ASE_Calendar.Calendar
{
    public class CalendarHelper
    {
        public string GetMonthdayString(int month)
        {
            string returnMonthString = "";

            switch (month)
            {

                case 1:
                    returnMonthString = "Jan";
                    break;

                case 2:
                    returnMonthString = "Feb";
                    break;

                case 3:
                    returnMonthString = "Mar";
                    break;

                case 4:
                    returnMonthString = "Apr";
                    break;

                case 5:
                    returnMonthString = "Mai";
                    break;

                case 6:
                    returnMonthString = "Jun";
                    break;

                case 7:
                    returnMonthString = "Jul";
                    break;

                case 8:
                    returnMonthString = "Aug";
                    break;

                case 9:
                    returnMonthString = "Sep";
                    break;

                case 10:
                    returnMonthString = "Okt";
                    break;

                case 11:
                    returnMonthString = "Nov";
                    break;

                case 12:
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

                case 1:
                    maxMonthDays = thirtyone;
                    break;

                case 2:
                    if (isLeapYear)
                    {
                        maxMonthDays = 29;
                    }
                    else
                    {
                        maxMonthDays = 28;
                    }
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

        private bool GetLeapYear(int year)
        {
            if (((year % 4 == 0) && (year % 100 != 0)) || (year % 400 == 0))
            {
                return true;
            }
            return false;
        }

        public string CalendarBuilderDays(int month, int year)
        {
            string calendar = null;
            
            Tuple<string> lastday = new Tuple<string>("15:00 - 16:00:");

            Tuple<string, string, string, string, string, string, string, Tuple<string>> appointmentsPerDayTuple =
                new Tuple<string, string, string, string, string, string, string, Tuple<string>>
                    ("07:00 - 08:00:",
                        "08:00 - 09:00:",
                        "09:00 - 10:00:",
                        "10:00 - 11:00:",
                        "11:00 - 12:00:",
                        "13:00 - 14:00:",
                        "14:00 - 15:00:",
                        lastday);

            IDictionary<int, Tuple<string, string, string, string, string, string, string, Tuple<string>>>
                appointmentsAndDayDict =
                    new Dictionary<int, Tuple<string, string, string, string, string, string, string, Tuple<string>>>();

            for (int i = 1; i <= GetMaxMonthDayInt(month, year); i++)
            {
                appointmentsAndDayDict.Add(i,appointmentsPerDayTuple);
            }

            for (int i = 1; i <= appointmentsAndDayDict.Count; i++)
            {
                calendar = calendar + i.ToString() + ":" + appointmentsAndDayDict[i].ToString() + "\n";
                
            }
            return calendar;
        }
    }
}