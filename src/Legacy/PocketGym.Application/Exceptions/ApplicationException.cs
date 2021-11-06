using System;
using System.Collections.Generic;
using System.Text;

namespace PocketGym.Application.Exceptions
{
    public abstract class ApplicationException : Exception
    {
        public string Reason { get; }
        public object InvalidData{ get; }

        protected ApplicationException(string reason, object invalidData)
        {
            Reason = reason;
            InvalidData = invalidData;
        }

        public abstract object ToResult();
    }
}
