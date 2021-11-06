using System;
using System.Runtime.Serialization;
using Newtonsoft.Json;

namespace PocketGym.Application.Core.Dtos
{
    public class LoginDto : IDataTransferObject
    {
        public string Username { get; set; }
        public string Password { get; set; }
        public string AccessToken { get; set; }
        public DateTime Expiration { get; set; }
        public DateTime Created { get; set; }
        public RoleDto[] Roles { get; set; }
        public string UserId { get; set; }
    }
}