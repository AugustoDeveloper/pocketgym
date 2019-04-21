using System;

namespace PocketGym.Application.Exceptions
{
    public class ValueAlreadyRegisteredException : Exception
    {
        public ValueAlreadyRegisteredException(string value) : base($"The {value} is already registered") { }
    }
}