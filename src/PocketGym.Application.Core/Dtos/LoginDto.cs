using System;
using System.Runtime.Serialization;

namespace PocketGym.Application.Core.Dtos
{
    public class LoginDto : IDataTransferObject
    {
        public string Username { get; set; }
        public string Password { get; set; }
        public string UserId { get; set;  }
        public string Token { get; set; }
        public DateTime ExpirationDate { get; set; }
        public RoleDto[] Roles { get; set; }
    }
}