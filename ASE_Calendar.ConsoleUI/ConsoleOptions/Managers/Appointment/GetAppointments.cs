using System;
using ASE_Calendar.Application.Services;
using ASE_Calendar.ConsoleUI.ConsoleOptions.Helpers;
using ASE_Calendar.Domain.Entities;

namespace ASE_Calendar.ConsoleUI.ConsoleOptions.Managers.Appointment
{
    /// <summary>
    ///     A class that laods the current appointmens
    /// </summary>
    public class GetAppointments
    {
        private readonly ConsoleColorHelper _colorHelper = new();
        public UserEntity CurrentUser;

        public GetAppointments(UserEntity currentUser)
        {
            CurrentUser = currentUser;
        }

        /// <summary>
        ///     A function that loads the appointments of the user and shows them on the ui.
        /// </summary>
        public void LoadAppointments()
        {
            Console.WriteLine("\nYour Appointments:\n");
            var appointmentData = AppointmentService.LoadAppointments(CurrentUser);

            if (appointmentData != null)
            {
                Console.WriteLine(appointmentData);
                Console.ReadLine();
            }
            else
            {
                _colorHelper.WriteLineRed("You don't have any appointments at the moment!");
                _colorHelper.WriteLineRed("Any key to continue!");
                Console.ReadLine();
            }
        }

        /// <summary>
        ///     A function that loads all appointments and shows them on the ui.
        /// </summary>
        public void LoadAllAppointments()
        {
            Console.WriteLine("\nAll Appointments:\n");
            var appointmentData = AppointmentService.LoadAllAppointments();

            if (appointmentData != null)
            {
                Console.WriteLine(appointmentData);
                Console.ReadLine();
            }
            else
            {
                _colorHelper.WriteLineRed("There are no appointments booked at the moment!");
                _colorHelper.WriteLineRed("Any key to continue!");
                Console.ReadLine();
            }
        }
    }
}