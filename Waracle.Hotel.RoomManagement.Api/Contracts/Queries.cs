
namespace Waracle.Hotel.RoomManagement.Api.Contracts
{
    public sealed record HotelDto(Guid Id, string Name);
    public sealed record RoomDto(Guid Id, string Name, string Category);
    public sealed record BookingDto(string ReferenceNumber, DateOnly From, DateOnly To, int Guests);
}
