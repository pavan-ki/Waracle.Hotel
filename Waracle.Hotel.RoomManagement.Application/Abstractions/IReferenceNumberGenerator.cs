using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Waracle.Hotel.RoomManagement.Application.Abstractions
{
    public interface IReferenceNumberGenerator
    {
        string Next();
    }
}
