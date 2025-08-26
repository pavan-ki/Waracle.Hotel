using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Waracle.Hotel.RoomManagement.Application.Models
{
    public class BookingResponse
    {
        public string ReferenceNumber { get; set; }
        public BookingError? Error { get; }

        public BookingResponse(string referenceNumber)
        {
            ReferenceNumber = referenceNumber;
        }

        public BookingResponse(BookingError error)
        {
            Error = error;
        }
    }
}
