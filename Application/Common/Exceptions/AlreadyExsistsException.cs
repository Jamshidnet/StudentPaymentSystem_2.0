using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentPaymentSystem.Application.Common.Exceptions
{
    public  class AlreadyExsistsException : Exception
    {
        public AlreadyExsistsException(string message) : base(message)
        {
            
        }
    }
}
