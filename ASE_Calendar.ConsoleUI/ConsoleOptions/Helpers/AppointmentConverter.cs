﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ASE_Calendar.Application.Services;
using ASE_Calendar.Domain.Entities;

namespace ASE_Calendar.ConsoleUI.ConsoleOptions.Helpers
{
    public class AppointmentConverter
    {
        private readonly CalendarHelperService _calendarHelper = new();

        public string ReturnUserAppointmentString(UserEntity user, Dictionary<int, Dictionary<int, AppointmentEntity>> appointmentDictionary)
        {
            string appointmentString = null;

            for (int i = 0 ; i <= 31; i++)
            {
                if (appointmentDictionary.ContainsKey(i))
                {
                    for (int j = 0; j <= 8; j++)
                    {
                        if (appointmentDictionary[i].ContainsKey(j))
                        {
                            if (appointmentDictionary[i][j] != null)
                            {
                                if (appointmentDictionary[i][j].UserId.Value == user.UserId.Value)
                                {
                                    appointmentString = appointmentString + appointmentDictionary[i][j].AppointmentData.Date.ToLongDateString()
                                                        + " " +
                                                        _calendarHelper.TimeSlotToTimeStamp(appointmentDictionary[i][j].AppointmentData
                                                            .TimeSlot)
                                                        + " " +
                                                        appointmentDictionary[i][j].AppointmentData.Description
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
            string appointmentString = null;

            for (int i = 0; i <= 31; i++)
            {
                if (appointmentDictionary.ContainsKey(i))
                {
                    for (int j = 0; j <= 8; j++)
                    {
                        if (appointmentDictionary[i].ContainsKey(j))
                        {
                            if (appointmentDictionary[i][j] != null)
                            {
                                
                                appointmentString = appointmentString + appointmentDictionary[i][j].AppointmentData.Date.ToLongDateString()
                                                    + " " +
                                                    _calendarHelper.TimeSlotToTimeStamp(appointmentDictionary[i][j].AppointmentData
                                                        .TimeSlot)
                                                    + " " +
                                                    appointmentDictionary[i][j].AppointmentData.Description
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
