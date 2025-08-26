using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Waracle.Hotel.RoomManagement.Domain.Abstractions;
using Waracle.Hotel.RoomManagement.Domain.ValueObjects;

namespace Waracle.Hotel.RoomManagement.Domain.Entities
{
    public sealed class Booking
    {
        private readonly List<Guest> _guests = new();

        public Guid Id { get; private set; }
        public string ReferenceNumber { get; private set; } = default!;
        public DateRange DateRange { get; private set; } = default!;
        public IReadOnlyCollection<Guest> Guests => _guests.AsReadOnly();

        private Booking()
        {

        }

        public Booking(Guid id, string referenceNumber, DateRange dateRange, IEnumerable<Guest> guests)
        {
            if (string.IsNullOrWhiteSpace(referenceNumber))
                throw new ArgumentException("Reference number is required.");

            if (guests is null)
                throw new ArgumentNullException("At least one guest is required.");

            if (!guests.Any())
                throw new DomainException("At least one guest is required.");

            Id = id;
            ReferenceNumber = referenceNumber;
            DateRange = dateRange;
            _guests = guests.ToList();
        }
    }
}
