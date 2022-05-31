namespace ASE_Calendar.ConsoleUI.ConsoleOptions.Helpers.InfoHelper
{
    public class InformationFactory
    {
        public static Information GetInformation(int role)
        {
            if (role == 0)
            {
                return new AdminInformation();
            }
            if (role == 1)
            {
                return new CarDealerInformation();
            }
            if (role == 2)
            {
                return new CustomerInformation();
            }

            return null;
        }
    }
}
