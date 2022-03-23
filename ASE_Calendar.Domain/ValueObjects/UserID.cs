using System;

namespace ASE_Calendar.Domain.ValueObjects
{
    public class UserId
    {
        public UserId(Guid value)
        {
            Value = value;
        }

        public Guid Value { get; init; }

        public override bool Equals(object obj)
        {
            return obj is UserId iD &&
                   Value.Equals(iD.Value);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Value);
        }
    }
}