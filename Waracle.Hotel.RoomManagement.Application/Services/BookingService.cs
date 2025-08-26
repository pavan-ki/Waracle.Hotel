using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Waracle.Hotel.RoomManagement.Application.Abstractions;
using Waracle.Hotel.RoomManagement.Application.Models;
using Waracle.Hotel.RoomManagement.Domain.Abstractions;
using Waracle.Hotel.RoomManagement.Domain.Entities;
using Waracle.Hotel.RoomManagement.Domain.Interfaces;
using Waracle.Hotel.RoomManagement.Domain.ValueObjects;

namespace Waracle.Hotel.RoomManagement.Application.Services
{
    public class BookingService
    {
        private readonly IRoomRepository _roomRepository;
        private readonly IReferenceNumberGenerator _referenceNumberGenerator;

        public BookingService(IRoomRepository roomRepository, IReferenceNumberGenerator referenceNumberGenerator)
        {
            _roomRepository = roomRepository;
            _referenceNumberGenerator = referenceNumberGenerator;
        }

        public async Task<BookingResponse> Book(Guid id, DateRange dateRange, IEnumerable<Guest> guests, CancellationToken cancellationToken = default)
        {
            var room = await _roomRepository.GetAsync(id, cancellationToken);
            if (room is null)
                return new BookingResponse(new BookingError("The selected room does not exist."));

            var referenceNumber = _referenceNumberGenerator.Next();

            try
            {
                room.AddBooking(referenceNumber, dateRange, guests);
                await _roomRepository.SaveAsync(room, cancellationToken);

                return new BookingResponse(referenceNumber);
            }
            catch (DomainException ex)
            {
                return new BookingResponse(new BookingError(ex.Message));
            }
            catch (ArgumentException ex)
            {
                return new BookingResponse(new BookingError(ex.Message));
            }
        }
    }
}
