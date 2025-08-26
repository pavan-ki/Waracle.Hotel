using Microsoft.EntityFrameworkCore;
using Waracle.Hotel.RoomManagement.Api.Contracts;
using Waracle.Hotel.RoomManagement.Api.Extensions;
using Waracle.Hotel.RoomManagement.Api.ResetServices;
using Waracle.Hotel.RoomManagement.Application.Services;
using Waracle.Hotel.RoomManagement.Domain.ValueObjects;
using Waracle.Hotel.RoomManagement.Infra.EfCore;
using DomainEntities = Waracle.Hotel.RoomManagement.Domain.Entities;

namespace Waracle.Hotel.RoomManagement.Api.Endpoints
{
    public static class BookingEndpoints
    {
        public static IEndpointRouteBuilder MapBookingEndpoints(this IEndpointRouteBuilder builder)
        {
            builder.MapGet("/hotels", async (BookingDbContext db, CancellationToken cancellationToken) =>
            {
                var result = await db.Hotels.AsNoTracking().ToListAsync(cancellationToken);

                return Results.Ok(result.ToHotels());
            });

            builder.MapGet("/hotels/search", async (string? query, BookingDbContext db, CancellationToken cancellationToken) =>
            {
                var result = await db.Hotels.AsNoTracking()
                                    .Where(h => EF.Functions.Like(h.Name, $"%{query}%"))
                                    .ToListAsync(cancellationToken);

                return Results.Ok(result.ToHotels());
            });

            builder.MapGet("/hotels/{id:guid}/rooms", async (Guid id, BookingDbContext db, CancellationToken cancellationToken) =>
            {
                var result = await db.Hotels
                                    .Where(h => h.Id == id)
                                    .SelectMany(r => r.Rooms).Include(r => r.RoomCategory)
                                    .AsNoTracking()
                                    .ToListAsync(cancellationToken);

                return Results.Ok(result.ToRooms());
            });

            builder.MapGet("/hotels/{id:guid}/availablerooms", async (Guid id, DateOnly from, DateOnly to, int noOfGuests, BookingDbContext db, CancellationToken cancellationToken) =>
            {
                var result = await db.Hotels
                                    .Where(h => h.Id == id)
                                    .SelectMany(r => r.Rooms).Include(r => r.RoomCategory)
                                    .AsNoTracking()
                                    .Where(r => r.RoomCategory.MaxCapacity >= noOfGuests
                                            && !r.Bookings.Any(b => b.DateRange.From == from && b.DateRange.To == to))
                                    .ToListAsync(cancellationToken);

                return Results.Ok(result.ToRooms());
            });

            builder.MapGet("/bookings/{referenceNumber:guid}/details", async (Guid referenceNumber, BookingDbContext db, CancellationToken cancellationToken) =>
            {
                var result = await db.Bookings
                                    .Include(b => b.Guests).Include(b => b.DateRange)
                                    .AsNoTracking()
                                    .Where(b => b.ReferenceNumber == referenceNumber.ToString())
                                    .ToListAsync(cancellationToken);

                return Results.Ok(result.ToBookings());
            });

            builder.MapPost("/rooms/{id:guid}/book", async (Guid id, BookingRequest request, BookingService service, CancellationToken cancellationToken) =>
            {
                if (request is null)
                    return Results.BadRequest("Request body is required.");

                if (request.From >= request.To)
                    return Results.BadRequest("From date should be less than To date.");

                if (request.NoOfGuests is null || !request.NoOfGuests.Any())
                    return Results.BadRequest("Atleast one guest is required.");

                var result = await service.Book(id, new DateRange(request.From, request.To), request.NoOfGuests.ToGuests(), cancellationToken);
                if (string.IsNullOrWhiteSpace(result.ReferenceNumber))
                    return Results.BadRequest(new { error = result.Error });

                return Results.Ok(new { referenceNumber = result.ReferenceNumber });
            });

            builder.MapPost("/reset", async (Reset service, CancellationToken cancellationToken) =>
            {
                await service.ResetAsync(cancellationToken);

                return Results.Ok(new { response = "Reset successful." });
            });

            return builder;
        }
    }
}
