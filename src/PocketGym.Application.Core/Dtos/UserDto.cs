using System;
namespace PocketGym.Application.Core.Dtos
{
    public class UserDto : IDataTransferObject
    {

        public long? Id { get; set; }
        public string Name { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public Int16 Age { get; set; }
    }
}