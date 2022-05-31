using ASE_Calendar.Application.Repositories;
using ASE_Calendar.ConsoleUI.ConsoleOptions.Helpers;
using ASE_Calendar.ConsoleUI.ConsoleOptions.Helpers.ConsoleColor;
using ASE_Calendar.Domain.Entities;
using System;

namespace ASE_Calendar.ConsoleUI.ConsoleOptions.Managers.Appointment
{
    /// <summary>
    ///     A class that loads the current appointments
    /// </summary>
    public class LoadAppointment
    {
        private readonly ConsoleColorGreen _consoleColorGreen = new();
        private readonly ConsoleColorRed _consoleColorRed = new();
        public UserEntity CurrentUser;

        public LoadAppointment(UserEntity currentUser)
        {
            CurrentUser = currentUser;
        }

        /// <summary>
        ///     A function that loads the appointments of the user and shows them on the ui.
        /// </summary>
        public void LoadAppointments()
        {
            Console.WriteLine("\nYour Appointments:\n");
            var appointmentRepository = new AppointmentRepository();
            var appointmentDict = appointmentRepository.ReturnAllAppointmentsDict();

            AppointmentConverterHelper appointmentConverter = new();
            string appointmentsString = appointmentConverter.ReturnUserAppointmentString(CurrentUser, appointmentDict);

            if (appointmentsString != null)
            {
                Console.WriteLine(appointmentsString);
                _consoleColorGreen.WriteLine("Any key to continue!");
                Console.ReadLine();
            }
            else
            {
                _consoleColorRed.WriteLine("You don't have any appointments at the moment!");
                _consoleColorRed.WriteLine("Any key to continue!");
                Console.ReadLine();
            }
        }

        /// <summary>
        ///     A function that loads all appointments and shows them on the ui.
        /// </summary>
        public void LoadAllAppointments()
        {
            Console.WriteLine("\nAll Appointments:\n");
            var appointmentRepository = new AppointmentRepository();
            var appointmentDict = appointmentRepository.ReturnAllAppointmentsDict();

            AppointmentConverterHelper appointmentConverter = new();
            string appointmentsString = appointmentConverter.ReturnAllAppointmentsString(appointmentDict);

            if (appointmentsString != null)
            {
                Console.WriteLine(appointmentsString);
                _consoleColorGreen.WriteLine("Any key to continue!");
                Console.ReadLine();
            }
            else
            {
                _consoleColorRed.WriteLine("There are no appointments booked at the moment!");
                _consoleColorRed.WriteLine("Any key to continue!");
                Console.ReadLine();
            }
        }
    }
}