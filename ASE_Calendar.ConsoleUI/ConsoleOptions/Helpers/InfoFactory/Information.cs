namespace ASE_Calendar.ConsoleUI.ConsoleOptions.Helpers.InfoHelper
{
    public abstract class Information
    {
        public abstract string GetInformationForRole();

        public string ShowInformation()
        {
            return GetInformationForRole();
        }

    }
}
