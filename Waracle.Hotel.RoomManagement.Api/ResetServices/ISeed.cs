using Microsoft.EntityFrameworkCore;
using Waracle.Hotel.RoomManagement.Domain.Entities;
using Waracle.Hotel.RoomManagement.Infra.EfCore;

namespace Waracle.Hotel.RoomManagement.Api.ResetServices
{
    public interface ISeed
    {
        Task SeedAsync(BookingDbContext db, CancellationToken cancellationToken);
    }
}
