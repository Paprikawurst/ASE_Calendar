namespace ASE_Calendar.ConsoleUI.ConsoleOptions.Helpers.InfoHelper
{
    public class CarDealerInformation : Information
    {
        public override string GetInformationForRole()
        {

            string carDealerInformation = "\n\nYou are a car dealer!\n\n" +
                                      "You are allowed to:\n" +
                                      "- List all appointments\n" +
                                      "- Edit all appointments \n" +
                                      "- Delete all appointments\n\n" +
                                      "Additional information: \n" +
                                      "- The green appointments in the calendar are yours\n" +
                                      "- The red appointments are from other users and blocked for you\n\n" +
                                      "Press any key to continue...";

            return carDealerInformation;

        }
    }
}
