using System;
using ASE_Calendar.Domain.Entities;

namespace ASE_Calendar.ConsoleUI.ConsoleOptions
{
    class InfoHelper
    {
        public void ShowInfo(UserEntity user)
        {
            if (user.UserDataRegistered.RoleId == 0)
            {
                Console.WriteLine("\nYou are an admin\n");
                Console.ReadKey();
            }
            else if (user.UserDataRegistered.RoleId == 1)
            {
                Console.WriteLine("\nYou are a car dealer\n");
                Console.ReadKey();
            }
            else if (user.UserDataRegistered.RoleId == 2)
            {
                Console.WriteLine("\nYou are a customer\n");
                Console.ReadKey();
            }
        }
    }
}
