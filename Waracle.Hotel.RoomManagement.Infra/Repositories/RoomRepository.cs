using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Waracle.Hotel.RoomManagement.Domain.Entities;
using Waracle.Hotel.RoomManagement.Domain.Interfaces;
using Waracle.Hotel.RoomManagement.Infra.EfCore;

namespace Waracle.Hotel.RoomManagement.Infra.Repositories
{
    public sealed class RoomRepository : IRoomRepository
    {
        private readonly BookingDbContext _bookingDbContext;

        public RoomRepository(BookingDbContext bookingDbContext)
        {
            _bookingDbContext = bookingDbContext;
        }

        public async Task<Room?> GetAsync(Guid id, CancellationToken cancellationToken = default)
        {
            return await _bookingDbContext.Rooms
                .Include(c => c.RoomCategory)
                .Include(b => b.Bookings)
                    .ThenInclude(g => g.Guests)
                .FirstOrDefaultAsync(r => r.Id == id, cancellationToken);
        }

        public async Task SaveAsync(Room room, CancellationToken cancellationToken = default)
        {
            if (_bookingDbContext.Entry(room).State == EntityState.Detached)
            {
                foreach (var booking in room.Bookings)
                {
                    var entry = _bookingDbContext.Entry(booking);
                    if (entry.State == EntityState.Detached || entry.State == EntityState.Unchanged)
                        entry.State = EntityState.Added;
                }
            }

            await _bookingDbContext.SaveChangesAsync(cancellationToken);
        }
    }
}
