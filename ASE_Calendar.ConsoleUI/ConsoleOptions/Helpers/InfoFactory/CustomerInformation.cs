namespace ASE_Calendar.ConsoleUI.ConsoleOptions.Helpers.InfoHelper
{
    public class CustomerInformation : Information
    {
        public override string GetInformationForRole()
        {

            string customerInformation = "\n\nYou are a customer!\n\n" +
                                      "You are allowed to:\n" +
                                      "- Create appointments\n" +
                                      "- List your appointments\n\n" +
                                      "Additional information: \n" +
                                      "- The green appointments in the calendar are yours\n" +
                                      "- The red appointments are from other users and blocked for you\n\n" +
                                      "Press any key to continue...";

            return customerInformation;

        }
    }
}
