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
    public class ChangeDescriptionOfAnAppointment
    {
        private readonly ConsoleColorHelper _colorHelper = new();

        public ChangeDescriptionOfAnAppointment()
        {
            ChangeDescriptionAppointmentSate changeDescriptionAppointmentSate = ChangeDescriptionAppointmentSate.UserInputId;
            Guid appointmentGuid = Guid.Empty;
            var inputGuidString = "";
            var appointmentDescription = "";

            switch (changeDescriptionAppointmentSate)
            {
                case ChangeDescriptionAppointmentSate.UserInputId:

                    ShowAppointmentsOnConsole();
                    Console.WriteLine("Enter the appointment ID you want to change the description:");
                    inputGuidString = Console.ReadLine();

                    goto case ChangeDescriptionAppointmentSate.CheckInputId;

                case ChangeDescriptionAppointmentSate.UserInputDescription:

                    Console.WriteLine("Enter the new description (max 25 token):");
                    appointmentDescription = Console.ReadLine();

                    goto case ChangeDescriptionAppointmentSate.CheckInputDescription;

                case ChangeDescriptionAppointmentSate.CheckInputId:

                    bool isValid = Guid.TryParse(inputGuidString, out appointmentGuid);

                    if (isValid)
                    {
                        goto case ChangeDescriptionAppointmentSate.UserInputDescription;
                    }

                    _colorHelper.WriteLineRed("Please enter a valid guid!");

                    goto case ChangeDescriptionAppointmentSate.UserInputId;

                case ChangeDescriptionAppointmentSate.CheckInputDescription:

                    if (string.IsNullOrEmpty(appointmentDescription) || appointmentDescription.Length > 25)
                    {
                        _colorHelper.WriteLineRed("Please enter a valid description!");
                        goto case ChangeDescriptionAppointmentSate.UserInputDescription;
                    }
                    else
                    {
                        goto case ChangeDescriptionAppointmentSate.changeDescription;
                    }

                case ChangeDescriptionAppointmentSate.changeDescription:

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
