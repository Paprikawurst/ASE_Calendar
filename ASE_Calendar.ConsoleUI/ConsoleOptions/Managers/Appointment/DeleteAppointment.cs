using System;
using ASE_Calendar.Application.Repositories;
using ASE_Calendar.ConsoleUI.ConsoleOptions.Helpers;
using ASE_Calendar.ConsoleUI.ConsoleOptions.Helpers.ConsoleColor;
using ASE_Calendar.ConsoleUI.Enums;

namespace ASE_Calendar.ConsoleUI.ConsoleOptions.Managers.Appointment
{
    /// <summary>
    ///     A class which starts the process on the ui to delete an appointment.
    /// </summary>
    public class DeleteAppointment
    {
        private readonly ConsoleColorGreen _consoleColorGreen = new();
        private readonly ConsoleColorRed _consoleColorRed = new();

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
                    AppointmentConverterHelper appointmentConverter = new();
                    string appointmentsString = appointmentConverter.ReturnAllAppointmentsString(appointmentDict);

                    if (appointmentsString == null)
                    {
                        _consoleColorRed.WriteLine("\nThere are no appointments at the moment!");
                        _consoleColorRed.WriteLine("Any key to continue.");
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

                    _consoleColorRed.WriteLine("Please enter a valid guid!");

                    goto case DeleteAppointmentState.UserInputId;

                case DeleteAppointmentState.DeleteAppointment:
                    var successfulDeletion = appointmentRepository.DeleteAppointment(appointmentGuid);
                    if (successfulDeletion)
                    {
                        _consoleColorGreen.WriteLine("The appointment has been deleted!");
                        _consoleColorGreen.WriteLine("Any key to continue!");
                        Console.ReadLine();
                    }
                    else
                    {
                        _consoleColorRed.WriteLine("There are no appointments booked at the moment!");
                        _consoleColorRed.WriteLine("Any key to continue!");
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
            AppointmentConverterHelper appointmentConverter = new();
            string appointmentsString = appointmentConverter.ReturnAllAppointmentsString(appointmentDict);

            Console.WriteLine("\n\n" + appointmentsString + "\n");
        }
    }
}