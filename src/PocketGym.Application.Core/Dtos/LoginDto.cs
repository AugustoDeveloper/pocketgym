using System;
namespace PocketGym.Application.Core.Dtos
{
    public class LoginDto : IDataTransferObject
    {
        public string Username { get; set; }
        public string Password { get; set; }
    }
}