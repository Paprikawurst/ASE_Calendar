using ASE_Calendar.ConsoleUI.ConsoleOptions;

namespace ASE_Calendar.ConsoleUI
{
    internal class Program
    {
        private static void Main()
        {
            HandleStateMachine stateMachine = HandleStateMachine.GetInstance();
            stateMachine.StartStateMachine();
        }
    }
}