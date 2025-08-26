using Waracle.Hotel.RoomManagement.Api.Contracts;
using Waracle.Hotel.RoomManagement.Domain.ValueObjects;
using DomainEntities = Waracle.Hotel.RoomManagement.Domain.Entities;

namespace Waracle.Hotel.RoomManagement.Api.Extensions
{
    public static class BookingExtensions
    {
        public static HotelDto ToHotel(this DomainEntities.Hotel hotel)
        {
            return new HotelDto(hotel.Id, hotel.Name);
        }

        public static List<HotelDto> ToHotels(this List<DomainEntities.Hotel> rooms)
        {
            return rooms.Select(ToHotel).ToList();
        }

        public static RoomDto ToRoom(this DomainEntities.Room room)
        {
            return new RoomDto(room.Id, room.Name, room.RoomCategory.Name);
        }

        public static List<RoomDto> ToRooms(this List<DomainEntities.Room> rooms)
        {
            return rooms.Select(ToRoom).ToList();
        }

        public static BookingDto ToBooking(this DomainEntities.Booking booking)
        {
            return new BookingDto(booking.ReferenceNumber, booking.DateRange.From, booking.DateRange.To, booking.Guests.Count);
        }

        public static List<BookingDto> ToBookings(this List<DomainEntities.Booking> bookings)
        {
            return bookings.Select(ToBooking).ToList();
        }

        public static Guest ToGuest(this GuestDto guest)
        {
            return new Guest(guest.FirstName, guest.LastName, guest.Email);
        }

        public static List<Guest> ToGuests(this List<GuestDto> guests)
        {
            return guests.Select(ToGuest).ToList();
        }
    }
}
