using Microsoft.EntityFrameworkCore;
using Waracle.Hotel.RoomManagement.Infra.EfCore;

namespace Waracle.Hotel.RoomManagement.Api.ResetServices
{
    public sealed class Reset
    {
        private readonly BookingDbContext _db;
        private readonly ISeed _seed;

        public Reset(BookingDbContext db, ISeed seed)
        {
            _db = db;
            _seed = seed;
        }

        public async Task ResetAsync(CancellationToken cancellationToken = default)
        {
            await using var transaction = await _db.Database.BeginTransactionAsync(cancellationToken);

            await _db.Database.ExecuteSqlRawAsync("DELETE FROM [BookingGuests]", cancellationToken);
            await _db.Database.ExecuteSqlRawAsync("DELETE FROM [Bookings]", cancellationToken);

            await transaction.CommitAsync();
        }
    }
}
