using System;
using System.Collections.Generic;

namespace PocketGym.Domain.Core.Entities
{
    public class User : IEntity
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Username { get; set; }
        public byte[] PasswordHash { get; set; }
        public byte[] PasswordSalt { get; set; }
        public UInt16 Age { get; set; }
        public List<Role> Roles { get; set; } = new List<Role>();
        public List<Serie> Series { get; set; } = new List<Serie>();
    }
}