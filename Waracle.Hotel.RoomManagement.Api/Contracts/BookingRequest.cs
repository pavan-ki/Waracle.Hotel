namespace Waracle.Hotel.RoomManagement.Api.Contracts
{
    public sealed record GuestDto(string FirstName, string LastName, string Email);
    public sealed record BookingRequest(DateOnly From, DateOnly To, List<GuestDto> NoOfGuests);
}
