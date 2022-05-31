namespace ASE_Calendar.ConsoleUI.ConsoleOptions.Helpers.ConsoleColor
{
    /// <summary>
    ///     Defines the methods a ConsoleColor class has to implement.
    /// </summary>
    public abstract class ConsoleColorWriter
    {
        public abstract void Write(string input);
        public abstract void WriteLine(string input);
    }
}
