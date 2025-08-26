using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Waracle.Hotel.RoomManagement.Domain.Abstractions
{
    public sealed class DomainException : Exception
    {
        public DomainException(string messsage)
            : base(messsage)
        {

        }
    }
}
