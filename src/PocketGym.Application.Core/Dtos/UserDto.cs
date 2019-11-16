using System;

namespace PocketGym.Application.Core.Dtos
{
    public class UserDto : IDataTransferObject
    {

        public string Id { get; set; }
        public string Name { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public UInt16 Age { get; set; }
    }
}