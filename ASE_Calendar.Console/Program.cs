using System;
using ASE_Calendar.Application.Services;
using ASE_Calendar.Domain.Entities;
using ASE_Calendar.ConsoleUI.ConsoleOptions;

namespace ASE_Calendar.ConsoleUI
{
    class Program
    {
        static void Main()
        {
            HandleStateMaschine stateMaschine = new HandleStateMaschine();
            stateMaschine.StartStateMaschine();
        }

    }

    
}
