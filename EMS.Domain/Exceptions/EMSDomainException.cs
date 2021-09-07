using System;
using System.Collections.Generic;
using System.Text;

namespace EMS.Domain.Exceptions
{
    /// <summary>
    /// Exception type for domain exceptions
    /// </summary>
    public class EMSDomainException : Exception
    {
        public EMSDomainException()
        { }

        public EMSDomainException(string message)
            : base(message)
        { }

        public EMSDomainException(string message, Exception innerException)
            : base(message, innerException)
        { }
    }
}
