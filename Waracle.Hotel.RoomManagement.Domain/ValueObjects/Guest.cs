using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Waracle.Hotel.RoomManagement.Domain.ValueObjects
{
    public sealed class Guest : IEquatable<Guest>
    {
        public string FirstName { get; }
        public string LastName { get; }
        public string Email { get; }

        private Guest()
        {

        }

        public Guest(string firstName, string lastName, string email)
        {
            if (string.IsNullOrWhiteSpace(firstName)
                || string.IsNullOrWhiteSpace(lastName)
                || string.IsNullOrWhiteSpace(email))
                throw new ArgumentException();

            FirstName = firstName;
            LastName = lastName;
            Email = email;
        }

        public bool Equals(Guest? other)
        {
            if (other is  null)
                return false;

            if (ReferenceEquals(this, other))
                return true;

            return FirstName.Equals(other.FirstName, StringComparison.OrdinalIgnoreCase)
                    && LastName.Equals(other.LastName, StringComparison.OrdinalIgnoreCase)
                    && Email.Equals(other.Email, StringComparison.OrdinalIgnoreCase);
        }

        public override bool Equals(object? obj) => Equals(obj as Guest);

        public override int GetHashCode() => HashCode.Combine(FirstName.ToLowerInvariant(), LastName.ToLowerInvariant(), Email.ToLowerInvariant());
    }
}
