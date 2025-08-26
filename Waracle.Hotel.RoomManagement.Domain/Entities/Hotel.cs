using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Waracle.Hotel.RoomManagement.Domain.Abstractions;
using Waracle.Hotel.RoomManagement.Domain.ValueObjects;

namespace Waracle.Hotel.RoomManagement.Domain.Entities
{
    public sealed class Hotel
    {
        private readonly List<Room> _rooms = new();

        public Guid Id { get; private set; }
        public string Name { get; private set; } = default!;
        public int Capacity { get; } = 6;
        public IReadOnlyCollection<Room> Rooms => _rooms.AsReadOnly();

        private Hotel()
        {

        }

        public Hotel(Guid id, string name)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentNullException(nameof(name));

            Id = id;
            Name = name;
        }

        public Room AddRoom(string name, RoomCategory roomCategory)
        {
            var room = new Room(Guid.NewGuid(), name, roomCategory);
            _rooms.Add(room);

            return room;
        }
    }
}
