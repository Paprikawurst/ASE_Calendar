using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using ASE_Calendar.Application.Repositories;
using ASE_Calendar.Application.Services;
using ASE_Calendar.ConsoleUI.Enums;
using ASE_Calendar.Domain.Entities;
using Microsoft.VisualBasic.CompilerServices;

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

        public void ChangeDateOfAnAppointment(DateTime selectedTime)
        {
            ChangeDateAppointmentSate changeDateAppointmentSate = ChangeDateAppointmentSate.UserInputId;
            Guid appointmentGuid = Guid.Empty;
            DateTime newDateTime = new DateTime();
            var inputGuidString = "";
            var appointmentDayString = "";
            var appointmentMonthString = "";
            var appointmentYearString = "";
            var appointmentTimeSlotString = "";
            short appointmentDayInt = 0;
            short appointmentMonthInt = 0;
            short appointmentYearInt = 0;

            switch (changeDateAppointmentSate)
            {
                case ChangeDateAppointmentSate.UserInputId:

                    ShowAppointmentsOnConsole();
                    Console.WriteLine("Enter the appointment ID you want to change the date:");
                    inputGuidString = Console.ReadLine();
                    goto case ChangeDateAppointmentSate.CheckInputId;
                  
                case ChangeDateAppointmentSate.UserInputDay:

                    Console.WriteLine("Enter the new day:");
                    appointmentDayString = Console.ReadLine();

                    goto case ChangeDateAppointmentSate.CheckInputDay;

                case ChangeDateAppointmentSate.UserInputMonth:

                    Console.WriteLine("Enter the new month:");
                    appointmentMonthString = Console.ReadLine();

                    goto case ChangeDateAppointmentSate.CheckInputMonth;
                    
                case ChangeDateAppointmentSate.UserInputYear:

                    Console.WriteLine("Enter the new year:");
                    appointmentYearString = Console.ReadLine();

                    goto case ChangeDateAppointmentSate.CheckInputYear;

                case ChangeDateAppointmentSate.UserInputTimeSlot:

                    Console.WriteLine(
                        "Please select a timeslot which is free on the selected day:\n08:00 - 09:00 = 1\n09:00 - 10:00 = 2\n10:00 - 11:00 = 3\n11:00 - 12:00 = 4\n13:00 - 14:00 = 5\n14:00 - 15:00 = 6\n15:00 - 16:00 = 7\n16:00 - 17:00 = 8\n");
                    appointmentTimeSlotString = Console.ReadLine();

                    goto case ChangeDateAppointmentSate.CheckInputTimeSlot;

                case ChangeDateAppointmentSate.CheckInputId:

                    bool isValid = Guid.TryParse(inputGuidString, out appointmentGuid);

                    if (isValid)
                    {
                        goto case ChangeDateAppointmentSate.UserInputDay;
                    }

                    _colorHelper.WriteLineRed("Please enter a valid guid!");

                    goto case ChangeDateAppointmentSate.UserInputId;

                case ChangeDateAppointmentSate.CheckInputDay:

                    bool isValidDay = short.TryParse(appointmentDayString, out appointmentDayInt);

                    if (!isValidDay || appointmentDayInt > 31 || appointmentDayInt <= 0)
                    {
                        _colorHelper.WriteLineRed("Please enter a valid day!");
                        goto case ChangeDateAppointmentSate.UserInputDay;
                    }

                    goto case ChangeDateAppointmentSate.UserInputMonth;

                case ChangeDateAppointmentSate.CheckInputMonth:

                    bool isValidMonth = short.TryParse(appointmentMonthString, out appointmentMonthInt);

                    if (!isValidMonth || appointmentMonthInt > 12 || appointmentMonthInt <= 0)
                    {
                        _colorHelper.WriteLineRed("Please enter a valid month!");
                        goto case ChangeDateAppointmentSate.UserInputMonth;
                    }

                    goto case ChangeDateAppointmentSate.UserInputYear;

                case ChangeDateAppointmentSate.CheckInputYear:

                    bool isValidYear = short.TryParse(appointmentYearString, out appointmentYearInt);

                    if (!isValidYear || appointmentYearInt < selectedTime.Year)
                    {
                        _colorHelper.WriteLineRed("Please enter a valid year!");
                        goto case ChangeDateAppointmentSate.UserInputYear;
                    }

                    goto case ChangeDateAppointmentSate.CheckInputDate;

                case ChangeDateAppointmentSate.CheckInputTimeSlot:

                    bool isNumber = Regex.IsMatch(appointmentTimeSlotString, @"[1-8]");

                    if (!isNumber || appointmentTimeSlotString == "")
                    {
                        _colorHelper.WriteLineRed("\n" + "Please enter a correct time slot!" + "\n");
                        goto case ChangeDateAppointmentSate.UserInputTimeSlot;
                    }

                    if (!AppointmentService.CheckIfTimeSlotIsFree(newDateTime, short.Parse(appointmentTimeSlotString), appointmentDayInt))
                    {
                        _colorHelper.WriteLineRed("\n" + "Time slot is occupied!" + "\n");
                        goto case ChangeDateAppointmentSate.UserInputTimeSlot;
                    }

                    goto case ChangeDateAppointmentSate.ChangeDate;


                case ChangeDateAppointmentSate.CheckInputDate:

                    if (CalendarHelperService.GetMaxMonthDayInt(appointmentMonthInt, appointmentYearInt) < appointmentDayInt)
                    {
                        _colorHelper.WriteLineRed("Please enter a valid date!");
                        goto case ChangeDateAppointmentSate.UserInputDay;
                    }

                    newDateTime = new DateTime(appointmentYearInt, appointmentMonthInt, appointmentDayInt);

                    goto case ChangeDateAppointmentSate.UserInputTimeSlot;
                    
                case ChangeDateAppointmentSate.ChangeDate:

                    var successfulChangeDate = AppointmentService.ChangeDate(appointmentGuid, newDateTime);

                    if (successfulChangeDate)
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