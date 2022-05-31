using ASE_Calendar.Domain.ValueObjects;

namespace ASE_Calendar.Domain.Entities
{
    public interface IUserEntity
    {
        UserData UserDataRegistered { get; init; }
        UserId UserId { get; init; }

        bool Equals(object obj);
        int GetHashCode();
    }
}