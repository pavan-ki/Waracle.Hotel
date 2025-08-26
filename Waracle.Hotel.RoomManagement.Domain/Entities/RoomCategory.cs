using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Waracle.Hotel.RoomManagement.Domain.Entities
{
    public sealed class RoomCategory
    {
        public Guid Id { get; private set; }
        public string Name { get; private set; } = default!;
        public int MinCapacity { get; private set; }
        public int MaxCapacity { get; private set; }

        private RoomCategory()
        {

        }

        public RoomCategory(Guid id, string name, int minCapacity, int maxCapacity)
        {
            Id = id;
            Name = name;
            MinCapacity = minCapacity;
            MaxCapacity = maxCapacity;
        }
    }
}
