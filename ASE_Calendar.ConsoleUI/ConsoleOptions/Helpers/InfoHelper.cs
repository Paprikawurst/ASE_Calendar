using System;
using ASE_Calendar.Domain.Entities;

namespace ASE_Calendar.ConsoleUI.ConsoleOptions.Helpers
{
    internal class InfoHelper
    {
        /// <summary>
        ///     Shows the user on the console what he is allowed to do inside the application depending on his role
        /// </summary>
        /// <param name="user"></param>
        public void ShowInfo(UserEntity user)
        {
            //if user role is admin
            if (user.UserDataRegistered.RoleId == 0)
            {
                Console.WriteLine("\nYou are an admin\n");

                Console.WriteLine("You are allowed to:\n" +
                                  "- Create appointments\n- List all appointments" + "\n- List your appointments" +
                                  "\n- Edit all appointments \n- Delete all appointments\n\nPress any key to continue...");
                Console.ReadKey();
            }
            //if user role is car dealer
            else if (user.UserDataRegistered.RoleId == 1)
            {
                Console.WriteLine("\nYou are a car dealer\n");

                Console.WriteLine("You are allowed to:\n" +
                                  "- List all appointments" + "\n- Edit all appointments " +
                                  "\n- Delete all appointments\n\nPress any key to continue...");
                Console.ReadKey();
            }
            //if user role is customer
            else if (user.UserDataRegistered.RoleId == 2)
            {
                Console.WriteLine("\nYou are a customer\n");

                Console.WriteLine("You are allowed to:\n" +
                                  "- Create appointments\n- List your appointments" + "\nPress any key to continue...");
                Console.ReadKey();
            }
        }
    }
}