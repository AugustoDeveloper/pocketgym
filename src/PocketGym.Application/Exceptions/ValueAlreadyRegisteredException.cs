using System;

namespace PocketGym.Application.Exceptions
{
    public class ValueAlreadyRegisteredException : ApplicationException
    {
        public ValueAlreadyRegisteredException(string value) : base($"The {value} is already registered", null) { }

        public override object ToResult()
            => new
            {
                reason = this.Reason
            };
    }
}