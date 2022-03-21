using System;
using System.Collections.Generic;
using ASE_Calendar.Application.Repositories;
using ASE_Calendar.Domain.Entities;

namespace ASE_Calendar.Application.Services
{
    public class CalendarHelperService
    {
        public static string GetMonthdayString(int month)
        {
            var returnMonthString = "";

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

        private static bool GetLeapYear(int year)
        {
            if (year % 4 == 0 && year % 100 != 0 || year % 400 == 0) return true;

            return false;
        }

        public string CalendarBuilderDays(DateTime selectedDate, UserEntity currentUser)
        {
            string calendar = null;

            AppointmentRepository appointmentRepository = new(currentUser);
            var appointmentDict =
                appointmentRepository.ReadFromJsonFileReturnAppointmentDict(selectedDate);

            IDictionary<int, string> appointmentsAndDayDict = new Dictionary<int, string>();

            for (var i = 1; i <= GetMaxMonthDayInt(selectedDate.Month, selectedDate.Year); i++)
                if (appointmentDict.ContainsKey(i))
                {
                    if (appointmentDict[i].UserId.Value == currentUser.UserId.Value
                        && i == appointmentDict[i].AppointmentData.Date.Day
                        && appointmentDict[i].AppointmentData.Date.Month == selectedDate.Month
                        && appointmentDict[i].AppointmentData.Date.Year == selectedDate.Year)
                        appointmentsAndDayDict.Add(i,
                            " " + TimeSlotToTimeStamp(appointmentDict[i].AppointmentData.TimeSlot) + " " +
                            appointmentDict[i].AppointmentData.Description);
                    else
                        appointmentsAndDayDict.Add(i, "");
                }
                else
                {
                    appointmentsAndDayDict.Add(i, "");
                }

            for (var i = 1; i <= appointmentsAndDayDict.Count; i++)
                calendar = calendar + i + ":" + appointmentsAndDayDict[i] + "\n";

            return calendar;
        }

        public string TimeSlotToTimeStamp(int timeSlot)
        {
            switch (timeSlot)
            {
                case 1:
                    return "08:00 - 09:00";
                case 2:
                    return "09:00 - 10:00";
                case 3:
                    return "10:00 - 11:00";
                case 4:
                    return "11:00 - 12:00";
                case 5:
                    return "13:00 - 14:00";
                case 6:
                    return "14:00 - 15:00";
                case 7:
                    return "15:00 - 16:00";
                case 8:
                    return "16:00 - 17:00";
            }

            return "";
        }
    }
}