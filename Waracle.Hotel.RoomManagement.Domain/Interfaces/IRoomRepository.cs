using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Waracle.Hotel.RoomManagement.Domain.Entities;

namespace Waracle.Hotel.RoomManagement.Domain.Interfaces
{
    public interface IRoomRepository
    {
        Task<Room?> GetAsync(Guid id, CancellationToken cancellationToken);

        Task SaveAsync(Room room, CancellationToken cancellationToken);
    }
}
