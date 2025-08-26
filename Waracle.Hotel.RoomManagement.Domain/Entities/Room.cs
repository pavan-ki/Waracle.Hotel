using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Waracle.Hotel.RoomManagement.Domain.Abstractions;
using Waracle.Hotel.RoomManagement.Domain.ValueObjects;

namespace Waracle.Hotel.RoomManagement.Domain.Entities
{
    public sealed class Room
    {
        private readonly List<Booking> _bookings = new();

        public Guid Id { get; private set; }
        public string Name { get; private set; } = default!;
        public RoomCategory RoomCategory { get; private set; } = default!;
        public int Capacity { get; private set; }
        public IReadOnlyCollection<Booking> Bookings => _bookings.AsReadOnly();

        private Room()
        {

        }

        public Room(Guid id, string name, RoomCategory roomCategory)
        {
            if (string.IsNullOrWhiteSpace(name)) 
                throw new ArgumentException(nameof(name));

            if (roomCategory is null)
                throw new ArgumentNullException(nameof(roomCategory));

            Id = id;
            Name = name;
            RoomCategory = roomCategory;
        }

        public Booking AddBooking(string referenceNumber, DateRange dateRange, IEnumerable<Guest> guests)
        {
            if (guests is null)
                throw new ArgumentNullException("At least one guest is required.");

            var holdGuests = guests.ToList();

            if (!holdGuests.Any())
                throw new DomainException("At least one guest is required.");

            if (holdGuests.Count > RoomCategory.MaxCapacity)
                throw new DomainException($"Only {RoomCategory.MaxCapacity} guests can be added to a {RoomCategory.Name} room.");

            if (_bookings.Any(x => x.DateRange.DoOverlap(dateRange)))
                throw new DomainException("Booking not available for selected dates.");

            var booking = new Booking(Guid.NewGuid(), referenceNumber, dateRange, guests);
            _bookings.Add(booking);

            return booking;
        }
    }
}
