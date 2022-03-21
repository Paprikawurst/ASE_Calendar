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

            AppointmentRepository appointmentRepository = new();
            var appointmentDict =
                appointmentRepository.ReturnAppointmentDict(selectedDate, currentUser);

            IDictionary<int, string> appointmentsAndDayDict = new Dictionary<int, string>();

            for (var i = 1; i <= GetMaxMonthDayInt(selectedDate.Month, selectedDate.Year); i++)
                if (appointmentDict.ContainsKey(i))
                {
                    if (appointmentDict[i].UserId.Value == currentUser.UserId.Value
                        && i == appointmentDict[i].AppointmentData.Date.Day
                        && appointmentDict[i].AppointmentData.Date.Month == selectedDate.Month
                        && appointmentDict[i].AppointmentData.Date.Year == selectedDate.Year)
                    {
                        appointmentsAndDayDict.Add(i,
                            " " + TimeSlotToTimeStamp(appointmentDict[i].AppointmentData.TimeSlot) + " " +
                            appointmentDict[i].AppointmentData.Description);
                    }
                    else
                    {
                        appointmentsAndDayDict.Add(i, "");
                    }
                }
                else
                {
                    appointmentsAndDayDict.Add(i, "");
                }

            for (var i = 1; i <= appointmentsAndDayDict.Count; i++)
            {
                calendar = calendar + i + ":" + appointmentsAndDayDict[i] + "\n";
            }

            return calendar;
        }

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