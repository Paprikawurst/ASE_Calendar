using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ASE_Calendar.Domain.Entities;
using ASE_Calendar.Application.Services;

namespace ASE_Calendar.ConsoleUI.ConsoleOptions
{
    public class AppointmentManager
    {
        public DateTime Date { get; set; }
        public DateTime currentTime = DateTime.Now;
        public UserEntity currentUser;
        public DateTime dateSelected;
        public AppointmentManager(UserEntity currentUser, DateTime currentMonth)
        {
            this.currentUser = currentUser;
            this.dateSelected = currentMonth;
        }

        public bool CreateAppointment()
        {
           
            
            Console.WriteLine("Please enter a Day of the current month");

            var day = Console.ReadLine();

            Console.WriteLine("Please select a timeslot: 8-9 = 1, 9-10 = 2, 10-11 = 3, 11-12 = 4\n13-14 = 5, 14-15 = 6, 15-16 = 7, 16-17 = 8");
            var timeSlot = Console.ReadLine();

            Date = new DateTime(dateSelected.Year, dateSelected.Month, Int32.Parse(day));
            
            AppointmentEntity Appointment = new AppointmentEntity(Date, Int32.Parse(timeSlot), currentUser.userId);
            AppointmentService appointmentService = new AppointmentService();
            appointmentService.createAppointment(Appointment);

            return true;
        }

        public void LoadAppointments()
        {
            Console.WriteLine("Your Appointments:\n");
            AppointmentService appointmentService = new AppointmentService();
            var appointmentData = appointmentService.loadAppointments(currentUser);
            Console.WriteLine(appointmentData);
            Console.ReadLine();

        }
    }
}
