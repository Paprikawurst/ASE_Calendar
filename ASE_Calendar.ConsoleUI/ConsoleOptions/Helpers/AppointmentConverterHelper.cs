using System.Collections.Generic;
using ASE_Calendar.ConsoleUI.Services;
using ASE_Calendar.Domain.Entities;

namespace ASE_Calendar.ConsoleUI.ConsoleOptions.Helpers
{
    public class AppointmentConverterHelper
    {
        private readonly CalendarHelperService _calendarHelper = new();

        public string ReturnUserAppointmentString(UserEntity user, Dictionary<int, Dictionary<int, AppointmentEntity>> appointmentDictionary)
        {
            string appointmentString = null;

            for (int i = 1 ; i <= 31; i++)
            {
                if (appointmentDictionary.ContainsKey(i))
                {
                    for (int j = 1; j <= 8; j++)
                    {
                        if (appointmentDictionary[i].ContainsKey(j))
                        {
                            if (appointmentDictionary[i][j] != null)
                            {
                                if (appointmentDictionary[i][j].UserId.Value == user.UserId.Value)
                                {
                                    appointmentString = appointmentString + appointmentDictionary[i][j].AppointmentData.Date.ToLongDateString()
                                                    + ", " +
                                                     _calendarHelper.TimeSlotToTimeStamp(appointmentDictionary[i][j].AppointmentData
                                                        .TimeSlot)
                                                    + ", " +
                                                    appointmentDictionary[i][j].AppointmentData.Description
                                                    + ", " +
                                                    appointmentDictionary[i][j].AppointmentId.Value
                                                    + "\n";

                                }
                            }

                        }
                            
                    }
                    
                }
                
                
                
            }

            return appointmentString;

        }

        public string ReturnAllAppointmentsString(Dictionary<int, Dictionary<int, AppointmentEntity>> appointmentDictionary)
        {
            string appointmentString = "";

            for (int i = 1; i <= 31; i++)
            {
                if (appointmentDictionary.ContainsKey(i))
                {
                    for (int j = 1; j <= 8; j++)
                    {
                        if (appointmentDictionary[i].ContainsKey(j))
                        {
                            if (appointmentDictionary[i][j] != null)
                            {

                                appointmentString = appointmentString + appointmentDictionary[i][j].AppointmentData.Date.ToLongDateString()
                                                    + ", " +
                                                     _calendarHelper.TimeSlotToTimeStamp(appointmentDictionary[i][j].AppointmentData
                                                        .TimeSlot)
                                                    + ", " +
                                                    appointmentDictionary[i][j].AppointmentData.Description
                                                    + ", " +
                                                    appointmentDictionary[i][j].AppointmentId.Value
                                                    + "\n";

                            }

                        }

                    }

                }
            }

            return appointmentString;
        }
    }
}
