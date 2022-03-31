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
    public class DeleteAnAppointment
    {
        private readonly ConsoleColorHelper _colorHelper = new();
        public DeleteAnAppointment()
        {
            const DeleteAppointmentSate deleteAppointmentSate = DeleteAppointmentSate.UserInputId;
            Guid appointmentGuid = Guid.Empty;
            var inputGuidString = "";

            switch (deleteAppointmentSate)
            {
                case DeleteAppointmentSate.UserInputId:
                    ShowAppointmentsOnConsole();
                    Console.WriteLine("Enter the appointment ID you want to delete:");
                    inputGuidString = Console.ReadLine();
                    goto case DeleteAppointmentSate.CheckInputId;

                case DeleteAppointmentSate.CheckInputId:

                    bool isValid = Guid.TryParse(inputGuidString, out appointmentGuid);

                    if (isValid)
                    {
                        goto case DeleteAppointmentSate.DeleteAppointment;
                    }

                    _colorHelper.WriteLineRed("Please enter a valid guid!");

                    goto case DeleteAppointmentSate.UserInputId;

                case DeleteAppointmentSate.DeleteAppointment:
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
