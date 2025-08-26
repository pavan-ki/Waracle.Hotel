using Microsoft.EntityFrameworkCore;
using Waracle.Hotel.RoomManagement.Domain.Entities;
using Waracle.Hotel.RoomManagement.Infra.EfCore;

namespace Waracle.Hotel.RoomManagement.Api.ResetServices
{
    public sealed class Seed : ISeed
    {
        public async Task SeedAsync(BookingDbContext db, CancellationToken cancellationToken = default)
        {
            if (!await db.Hotels.AnyAsync(cancellationToken))
            {
                var singleRoom = new RoomCategory(Guid.NewGuid(), "Single", 1, 1);
                var doubleRoom = new RoomCategory(Guid.NewGuid(), "Double", 1, 2);
                var deluxeRoom = new RoomCategory(Guid.NewGuid(), "Deluxe", 1, 4);

                db.RoomCategories.AddRange(singleRoom, doubleRoom, deluxeRoom);

                var hotelA = new Domain.Entities.Hotel(Guid.NewGuid(), "Hotel A");

                hotelA.AddRoom("101", singleRoom);
                hotelA.AddRoom("102", singleRoom);
                hotelA.AddRoom("103", doubleRoom);
                hotelA.AddRoom("104", doubleRoom);
                hotelA.AddRoom("201", deluxeRoom);
                hotelA.AddRoom("202", deluxeRoom);

                var hotelB = new Domain.Entities.Hotel(Guid.NewGuid(), "Hotel B");

                hotelB.AddRoom("101", deluxeRoom);
                hotelB.AddRoom("102", deluxeRoom);
                hotelB.AddRoom("103", doubleRoom);
                hotelB.AddRoom("104", doubleRoom);
                hotelB.AddRoom("201", singleRoom);
                hotelB.AddRoom("202", singleRoom);

                db.Hotels.AddRange(hotelA, hotelB);
                await db.SaveChangesAsync();
            }
        }
    }
}
