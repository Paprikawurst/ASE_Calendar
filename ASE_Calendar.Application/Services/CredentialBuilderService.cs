using ASE_Calendar.Domain.Entities;

namespace ASE_Calendar.Application.Services
{
    public class CredentialBuilderService
    {
        public UserEntity UserEntity { get; set; }

        public CredentialBuilderService()
        {
        }

        public CredentialBuilderService(UserEntity user)
        {
            UserEntity = user;
        }
    }
}
