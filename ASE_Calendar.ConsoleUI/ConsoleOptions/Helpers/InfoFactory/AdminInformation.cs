namespace ASE_Calendar.ConsoleUI.ConsoleOptions.Helpers.InfoHelper
{
    public class AdminInformation : Information
    {

        public override string GetInformationForRole()
        {

            string adminInformation = "\n\nYou are an admin\n\n" +
                                    "You are allowed to:\n" +
                                    "- Create appointments\n" +
                                    "- List all appointments\n" +
                                    "- List your appointments\n" +
                                    "- Edit all appointments \n" +
                                    "- Delete all appointments\n\n" +
                                    "Additional information: \n" +
                                    "- The green appointments in the calendar are yours\n" +
                                    "- The red appointments are from other users and blocked for you\n\n" +
                                    "Press any key to continue...";

            return adminInformation;

        }
    }
}
