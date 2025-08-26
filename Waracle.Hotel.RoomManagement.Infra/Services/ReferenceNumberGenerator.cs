using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Waracle.Hotel.RoomManagement.Application.Abstractions;

namespace Waracle.Hotel.RoomManagement.Infra.Services
{
    public class ReferenceNumberGenerator : IReferenceNumberGenerator
    {
        public string Next()
        {
            return Guid.NewGuid().ToString();
        }
    }
}
