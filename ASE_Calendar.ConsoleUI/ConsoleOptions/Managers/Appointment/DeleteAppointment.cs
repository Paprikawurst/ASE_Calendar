﻿using System;
using ASE_Calendar.Application.Repositories;
using ASE_Calendar.Application.Services;
using ASE_Calendar.ConsoleUI.ConsoleOptions.Helpers;
using ASE_Calendar.ConsoleUI.Enums;

namespace ASE_Calendar.ConsoleUI.ConsoleOptions.Managers.Appointment
{
    /// <summary>
    ///     A class which starts the process on the ui to delete an appointment.
    /// </summary>
    public class DeleteAppointment
    {
        private readonly ConsoleColorHelper _colorHelper = new();

        public DeleteAppointment()
        {
            const DeleteAppointmentState deleteAppointmentSate = DeleteAppointmentState.CheckForAppointments;
            var appointmentGuid = Guid.Empty;
            var inputGuidString = "";
            var appointmentRepository = new AppointmentRepository();

            switch (deleteAppointmentSate)
            {
                case DeleteAppointmentState.CheckForAppointments:
                    var appointmentDict = appointmentRepository.ReturnAllAppointmentsDict(); ;
                    AppointmentConverter appointmentConverter = new AppointmentConverter();
                    string appointmentsString = appointmentConverter.ReturnAllAppointmentsString(appointmentDict);

                    if (appointmentsString == null)
                    {
                        _colorHelper.WriteLineRed("\nThere are no appointments at the moment!");
                        _colorHelper.WriteLineRed("Any key to continue.");
                        Console.ReadLine();
                        break;
                    }

                    goto case DeleteAppointmentState.UserInputId;
                case DeleteAppointmentState.UserInputId:
                    ShowAppointmentsOnConsole();
                    Console.WriteLine("\nEnter the appointment ID you want to delete:");
                    inputGuidString = Console.ReadLine();
                    goto case DeleteAppointmentState.CheckInputId;

                case DeleteAppointmentState.CheckInputId:

                    var isValid = Guid.TryParse(inputGuidString, out appointmentGuid);

                    if (isValid)
                    {
                        goto case DeleteAppointmentState.DeleteAppointment;
                    }

                    _colorHelper.WriteLineRed("Please enter a valid guid!");

                    goto case DeleteAppointmentState.UserInputId;

                case DeleteAppointmentState.DeleteAppointment:
                    var successfulDeletion = appointmentRepository.DeleteAppointment(appointmentGuid);
                    if (successfulDeletion)
                    {
                        _colorHelper.WriteLineGreen("The appointment has been deleted!");
                        _colorHelper.WriteLineGreen("Any key to continue!");
                        Console.ReadLine();
                    }
                    else
                    {
                        _colorHelper.WriteLineRed("There are no appointments booked at the moment!");
                        _colorHelper.WriteLineRed("Any key to continue!");
                        Console.ReadLine();
                    }

                    break;
            }
        }
        /// <summary>
        /// gets a list of appointment entities from the repository and outputs it to the console
        /// </summary>
        public static void ShowAppointmentsOnConsole()
        {
            var appointmentRepository = new AppointmentRepository();
            var appointmentDict = appointmentRepository.ReturnAllAppointmentsDict();
            AppointmentConverter appointmentConverter = new AppointmentConverter();
            string appointmentsString = appointmentConverter.ReturnAllAppointmentsString(appointmentDict);

            Console.WriteLine("\n\n" + appointmentsString + "\n");
        }
    }
}