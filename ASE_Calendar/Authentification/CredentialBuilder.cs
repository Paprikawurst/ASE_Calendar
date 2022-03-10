using ASE_Calendar.Entities;

namespace ASE_Calendar.Authentification
{
    class CredentialBuilder
    {
        public User user { get; set; }

        public CredentialBuilder()
        {
        }

        public CredentialBuilder(User user)
        {
            this.user = user;
        }
    }
}
