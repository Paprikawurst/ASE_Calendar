﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ASE_Calendar.Application.Repositories;
using ASE_Calendar.Application.Services;
using ASE_Calendar.ConsoleUI.ConsoleOptions.Helpers;
using ASE_Calendar.ConsoleUI.Enums;

namespace ASE_Calendar.ConsoleUI.ConsoleOptions.Managers.Appointment
{
 /// <summary>
 /// A class which starts the progress on the ui to delete an appointment.
 /// </summary>
    public class DeleteAppointment
    {
        private readonly ConsoleColorHelper _colorHelper = new();
        public DeleteAppointment()
        {
            const DeleteAppointmentState deleteAppointmentSate = DeleteAppointmentState.CheckForAppointments;
            Guid appointmentGuid = Guid.Empty;
            var inputGuidString = "";

            switch (deleteAppointmentSate)
            {
                case DeleteAppointmentState.CheckForAppointments:
                    AppointmentRepository appointmentRepository = new AppointmentRepository();

                    if (appointmentRepository.ReturnAllAppointmentsString() == null)
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

                    bool isValid = Guid.TryParse(inputGuidString, out appointmentGuid);

                    if (isValid)
                    {
                        goto case DeleteAppointmentState.DeleteAppointment;
                    }

                    _colorHelper.WriteLineRed("Please enter a valid guid!");

                    goto case DeleteAppointmentState.UserInputId;

                case DeleteAppointmentState.DeleteAppointment:
                    var successfulDeletion = AppointmentService.DeleteAnAppointment(appointmentGuid);
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

        public void ShowAppointmentsOnConsole()
        {
            var appointmentRepository = new AppointmentRepository();
            var allAppointments = appointmentRepository.ReturnAllAppointmentsString();

            Console.WriteLine("\n\n" + allAppointments + "\n");
        }
    }
}