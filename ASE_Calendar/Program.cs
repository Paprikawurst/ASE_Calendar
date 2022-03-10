﻿using System;

namespace ASE_Calendar
{
    class Program
    {
        static void Main()
        {
            //TODO: only works on Windows - comment out this line if a PlatformNotSupportedException appears
            //Console.SetWindowSize(150, 50);

            DateTime TimeNow = new DateTime();
            TimeNow = DateTime.Now;
            Calendar.Calendar Calendar = new Calendar.Calendar(TimeNow);
            Authentification.Authentification Auth = new Authentification.Authentification();
            var startProgram = true;

            Auth.StartAuthentification();
            Console.Clear();
            Calendar.CreateCalendarThisMonth();

            while (startProgram)
            {
                Console.WriteLine("Previous month: 1 | Next month: 2 | Close application: 3");
                var input = Console.ReadLine();
                Console.Clear();

                switch (input) 
                {
                    case "1":
                        Console.Clear();
                        Calendar.CreateCalendarPrevMonth();
                        break;
                    case "2":
                        Console.Clear();
                        Calendar.CreateCalendarNextMonth();
                        break;
                    case "3":
                        startProgram = false;
                        break;
                }

            }
        }
    }
}
