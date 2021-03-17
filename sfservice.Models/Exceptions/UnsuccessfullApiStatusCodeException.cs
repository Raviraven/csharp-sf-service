using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace sfservice.Domain.Exceptions
{
    public class UnsuccessfullApiStatusCodeException : Exception
    {
        public UnsuccessfullApiStatusCodeException(string message) : base(message)
        {
        }
    }
}
