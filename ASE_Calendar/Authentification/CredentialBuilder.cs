using ASE_Calendar.Entities;

namespace ASE_Calendar.Authentification
{
    class CredentialBuilder
    {
        public User User { get; set; }

        public CredentialBuilder()
        {
        }

        public CredentialBuilder(User User)
        {
            this.User = User;
        }
    }
}
