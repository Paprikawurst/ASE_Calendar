using System;
using ASE_Calendar.Application.Repositories;
using ASE_Calendar.ConsoleUI.ConsoleOptions.Helpers;
using ASE_Calendar.ConsoleUI.ConsoleOptions.Helpers.ConsoleColor;
using ASE_Calendar.ConsoleUI.Enums;

namespace ASE_Calendar.ConsoleUI.ConsoleOptions.Managers.Appointment
{
    /// <summary>
    ///     A class which starts the process on the ui to change the description of an appointment.
    /// </summary>
    public class ChangeAppointmentDescription
    {
        private readonly ConsoleColorGreen _consoleColorGreen = new();
        private readonly ConsoleColorRed _consoleColorRed = new();

        public ChangeAppointmentDescription()
        {
            var changeDescriptionAppointmentSate = ChangeAppointmentDescriptionState.CheckForAppointments;
            var appointmentGuid = Guid.Empty;
            var inputGuidString = "";
            var appointmentDescription = "";
            var appointmentRepository = new AppointmentRepository();
            
            switch (changeDescriptionAppointmentSate)
            {
                case ChangeAppointmentDescriptionState.CheckForAppointments:
                    
                    var appointmentDict = appointmentRepository.ReturnAllAppointmentsDict();
                    AppointmentConverterHelper appointmentConverter = new();
                    string appointmentsString = appointmentConverter.ReturnAllAppointmentsString(appointmentDict);

                    if (appointmentsString == null)
                    {
                        _consoleColorRed.WriteLine("\nThere are no appointments at the moment!");
                        _consoleColorRed.WriteLine("Any key to continue.");
                        Console.ReadLine();
                        break;
                    }

                    goto case ChangeAppointmentDescriptionState.UserInputId;

                case ChangeAppointmentDescriptionState.UserInputId:

                    ShowAppointmentsOnConsole();
                    Console.WriteLine("\nEnter the appointment ID you want to change the description:");
                    inputGuidString = Console.ReadLine();

                    goto case ChangeAppointmentDescriptionState.CheckInputId;

                case ChangeAppointmentDescriptionState.UserInputDescription:

                    Console.WriteLine("Enter the new description (max 25 token):");
                    appointmentDescription = Console.ReadLine();

                    goto case ChangeAppointmentDescriptionState.CheckInputDescription;

                case ChangeAppointmentDescriptionState.CheckInputId:

                    var isValid = Guid.TryParse(inputGuidString, out appointmentGuid);

                    if (isValid)
                    {
                        goto case ChangeAppointmentDescriptionState.UserInputDescription;
                    }

                    _consoleColorRed.WriteLine("Please enter a valid guid!");

                    goto case ChangeAppointmentDescriptionState.UserInputId;

                case ChangeAppointmentDescriptionState.CheckInputDescription:

                    if (string.IsNullOrEmpty(appointmentDescription) || appointmentDescription.Length > 25 || string.IsNullOrWhiteSpace(appointmentDescription))
                    {
                        _consoleColorRed.WriteLine("Please enter a valid description!");
                        goto case ChangeAppointmentDescriptionState.UserInputDescription;
                    }
                    else
                    {
                        goto case ChangeAppointmentDescriptionState.changeDescription;
                    }

                case ChangeAppointmentDescriptionState.changeDescription:

                    var successfulChangeDescription = appointmentRepository.ChangeDescription(appointmentGuid, appointmentDescription);

                    if (successfulChangeDescription)
                    {
                        _consoleColorGreen.WriteLine("The appointment has been edited!");
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