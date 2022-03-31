using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using ASE_Calendar.Application.Repositories;
using ASE_Calendar.Application.Services;
using ASE_Calendar.ConsoleUI.Enums;
using ASE_Calendar.Domain.Entities;

namespace ASE_Calendar.ConsoleUI.ConsoleOptions
{
    public class AppointmentManager
    {
        private readonly ConsoleColorHelper _colorHelper = new();
        public UserEntity CurrentUser;
        public DateTime DateSelected;

        public AppointmentManager(UserEntity currentUser, DateTime dateSelected)
        {
            CurrentUser = currentUser;
            DateSelected = dateSelected;
        }

        public DateTime Date { get; set; }

        public void CreateAppointment()
        {
            var day = "";
            var timeSlot = "";
            var description = "";

            CreateAppointmentState appointmentState = CreateAppointmentState.UserInputDay;

            switch (appointmentState)
            {
                case CreateAppointmentState.UserInputDay:

                    Console.WriteLine("Please enter a Day of the current month");
                    day = Console.ReadLine();
                    goto case CreateAppointmentState.CheckInputDay;

                case CreateAppointmentState.UserInputTimeSlot:

                    Console.WriteLine(
                        "Please select a timeslot which is free on the selected day:\n08:00 - 09:00 = 1\n09:00 - 10:00 = 2\n10:00 - 11:00 = 3\n11:00 - 12:00 = 4\n13:00 - 14:00 = 5\n14:00 - 15:00 = 6\n15:00 - 16:00 = 7\n16:00 - 17:00 = 8\n");
                    timeSlot = Console.ReadLine();
                    goto case CreateAppointmentState.CheckInputTimeSlot;

                case CreateAppointmentState.CheckInputDay:

                    var isNumber = Regex.IsMatch(day, @"^[0-9]*$");
                    var maxDays = CalendarHelperService.GetMaxMonthDayInt(DateSelected.Month, DateSelected.Year);
                    
                    if (!isNumber || day == "" || short.Parse(day) <= 0 || short.Parse(day) > maxDays)
                    {
                        _colorHelper.WriteLineRed("\n" + "Please enter the correct day!" + "\n");
                        goto case CreateAppointmentState.UserInputDay;
                    }

                    goto case CreateAppointmentState.UserInputTimeSlot;

                case CreateAppointmentState.CheckInputTimeSlot:

                    isNumber = Regex.IsMatch(timeSlot, @"[1-8]");
                    

                    if (!isNumber || timeSlot == "" ||
                        !AppointmentService.CheckIfTimeSlotIsFree(DateSelected, short.Parse(timeSlot), short.Parse(day)))
                    {
                        _colorHelper.WriteLineRed("\n" + "Please enter a correct time slot!" + "\n");
                        goto case CreateAppointmentState.UserInputTimeSlot;
                    }

                    goto case CreateAppointmentState.UserInputDescription;

                case CreateAppointmentState.UserInputDescription:

                    Console.WriteLine("Please enter a description of your appointment!. (Max 25 tokens)");
                    description = Console.ReadLine();

                    goto case CreateAppointmentState.CheckInputDescription;

                case CreateAppointmentState.CheckInputDescription:

                    if (description.Length > 25 || description == "")
                    {
                        _colorHelper.WriteLineRed("Wrong input! Enter more than 0 and less than 25 signs!");
                        goto case CreateAppointmentState.UserInputDescription;
                    }

                    break;
            }

            Date = new DateTime(DateSelected.Year, DateSelected.Month, int.Parse(day));

            AppointmentEntity appointment =
                new(Date, int.Parse(timeSlot), CurrentUser.UserId, Guid.NewGuid(), description);
            AppointmentService.CreateAppointment(appointment);
        }

        public void LoadAppointments()
        {
            Console.WriteLine("Your Appointments:\n");
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

        public void LoadAllAppointments()
        {
            Console.WriteLine("All Appointments:\n");
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

        public void DeleteAnAppointment(DateTime selectedTime)
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
                    var appointmentData = AppointmentService.DeleteAnAppointment(appointmentGuid);
                    if (appointmentData != null)
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

        public void ChangeDescriptionOfAnAppointment(DateTime selectedTime)
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

                    var appointmentData = AppointmentService.ChangeDescription(appointmentGuid, appointmentDescription);

                    if (appointmentData != null)
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

        public void ChangeDateOfAnAppointment(DateTime selectedTime)
        {
            ShowAppointmentsOnConsole();
            Console.WriteLine("Enter the appointment number you want to change the date of:");
            var appointmentGuid = Guid.Parse(Console.ReadLine());


            Console.WriteLine("Enter the new day:");
            var appointmentDay = Console.ReadLine();
            Console.WriteLine("Enter the new month:");
            var appointmentMonth = Console.ReadLine();
            Console.WriteLine("Enter the new year:");
            var appointmentYear = Console.ReadLine();

            var newDate = new DateTime(short.Parse(appointmentYear), short.Parse(appointmentMonth),
                short.Parse(appointmentDay));
            var appointmentData = AppointmentService.ChangeDate(appointmentGuid, newDate);

            if (appointmentData != null)
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
        }

        public void ShowAppointmentsOnConsole()
        {
            var appointmentRepository = new AppointmentRepository();
            var allAppointments = appointmentRepository.ReturnAllAppointmentsString();

            Console.WriteLine("\n\n" + allAppointments + "\n");
        }
    }
}