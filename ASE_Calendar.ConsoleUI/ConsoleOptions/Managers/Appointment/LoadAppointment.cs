using System;
using ASE_Calendar.ConsoleUI.ConsoleOptions.Helpers;
using ASE_Calendar.Domain.Entities;
using ASE_Calendar.Application.Repositories;

namespace ASE_Calendar.ConsoleUI.ConsoleOptions.Managers.Appointment
{
    /// <summary>
    ///     A class that loads the current appointments
    /// </summary>
    public class LoadAppointment
    {
        private readonly ConsoleColorHelper _colorHelper = new();
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

            AppointmentConverterHelper appointmentConverter = new AppointmentConverterHelper();
            string appointmentsString = appointmentConverter.ReturnUserAppointmentString(CurrentUser, appointmentDict);

            if (appointmentsString != null)
            {
                Console.WriteLine(appointmentsString);
                _colorHelper.WriteLineGreen("Any key to continue!");
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
            var appointmentRepository = new AppointmentRepository();
            var appointmentDict = appointmentRepository.ReturnAllAppointmentsDict();

            AppointmentConverterHelper appointmentConverter = new AppointmentConverterHelper();
            string appointmentsString = appointmentConverter.ReturnAllAppointmentsString(appointmentDict);

            if (appointmentsString != null)
            {
                Console.WriteLine(appointmentsString);
                _colorHelper.WriteLineGreen("Any key to continue!");
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