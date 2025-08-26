using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Waracle.Hotel.RoomManagement.Application.Models
{
    public class BookingError
    {
        public string? ErrorMessage { get; }
        public BookingError(string errorMessage)
        {
            ErrorMessage = errorMessage;
        }
    }
}
