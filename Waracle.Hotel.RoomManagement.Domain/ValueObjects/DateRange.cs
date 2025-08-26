using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Waracle.Hotel.RoomManagement.Domain.ValueObjects
{
    public sealed class DateRange : IEquatable<DateRange>
    {
        public DateOnly From { get; }
        public DateOnly To { get; }

        private DateRange()
        {

        }

        public DateRange(DateOnly from, DateOnly to)
        {
            if (from >= to)
                throw new ArgumentException();

            From = from;
            To = to;
        }

        public bool DoOverlap(DateRange other)
        {
            if (From < other.To
                && other.From < To)
                return true;

            return false;
        }

        public bool Equals(DateRange? other)
        {
            if (other is null)
                return false;

            if (ReferenceEquals(this, other))
                return true;

            return From.Equals(other.From)
                    && To.Equals(other.To);
        }

        public override bool Equals(object? obj) => Equals(obj as DateRange);

        public override int GetHashCode() => HashCode.Combine(From, To);
    }
}
