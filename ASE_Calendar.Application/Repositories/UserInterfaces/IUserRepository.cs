using ASE_Calendar.Domain.Entities;

namespace ASE_Calendar.Application.Repositories
{
    public interface IUserRepository
    {
        string Password { get; set; }
        string Username { get; set; }

        bool ReadFromJsonFileReturnTrueIfUsernameExists();
        UserEntity ReturnUserEntity();
    }
}