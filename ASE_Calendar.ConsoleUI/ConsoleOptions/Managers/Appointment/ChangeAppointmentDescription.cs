using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ASE_Calendar.Application.Repositories;
using ASE_Calendar.Application.Services;
using ASE_Calendar.ConsoleUI.Enums;

namespace ASE_Calendar.ConsoleUI.ConsoleOptions.Managers.Appointment
{
    /// <summary>
    /// A class which starts the progress on the ui to change the description of an appointment.
    /// </summary>
    public class ChangeAppointmentDescription
    {
        private readonly ConsoleColorHelper _colorHelper = new();

        public ChangeAppointmentDescription()
        {
            ChangeDescriptionAppointmentState changeDescriptionAppointmentSate = ChangeDescriptionAppointmentState.CheckForAppointments;
            Guid appointmentGuid = Guid.Empty;
            var inputGuidString = "";
            var appointmentDescription = "";

            switch (changeDescriptionAppointmentSate)
            {
                case ChangeDescriptionAppointmentState.CheckForAppointments:
                    AppointmentRepository appointmentRepository = new AppointmentRepository();

                    if (appointmentRepository.ReturnAllAppointmentsString() == null)
                    {
                        _colorHelper.WriteLineRed("\nThere are no appointments at the moment!");
                        _colorHelper.WriteLineRed("Any key to continue.");
                        Console.ReadLine();
                        break;
                    }
                    goto case ChangeDescriptionAppointmentState.UserInputId;

                case ChangeDescriptionAppointmentState.UserInputId:

                    ShowAppointmentsOnConsole();
                    Console.WriteLine("\nEnter the appointment ID you want to change the description:");
                    inputGuidString = Console.ReadLine();

                    goto case ChangeDescriptionAppointmentState.CheckInputId;

                case ChangeDescriptionAppointmentState.UserInputDescription:

                    Console.WriteLine("Enter the new description (max 25 token):");
                    appointmentDescription = Console.ReadLine();

                    goto case ChangeDescriptionAppointmentState.CheckInputDescription;

                case ChangeDescriptionAppointmentState.CheckInputId:

                    bool isValid = Guid.TryParse(inputGuidString, out appointmentGuid);

                    if (isValid)
                    {
                        goto case ChangeDescriptionAppointmentState.UserInputDescription;
                    }

                    _colorHelper.WriteLineRed("Please enter a valid guid!");

                    goto case ChangeDescriptionAppointmentState.UserInputId;

                case ChangeDescriptionAppointmentState.CheckInputDescription:

                    if (string.IsNullOrEmpty(appointmentDescription) || appointmentDescription.Length > 25)
                    {
                        _colorHelper.WriteLineRed("Please enter a valid description!");
                        goto case ChangeDescriptionAppointmentState.UserInputDescription;
                    }
                    else
                    {
                        goto case ChangeDescriptionAppointmentState.changeDescription;
                    }

                case ChangeDescriptionAppointmentState.changeDescription:

                    var successfulChangeDescription = AppointmentService.ChangeDescription(appointmentGuid, appointmentDescription);

                    if (successfulChangeDescription)
                    {
                        _colorHelper.WriteLineGreen("The appointment has been edited!");
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
