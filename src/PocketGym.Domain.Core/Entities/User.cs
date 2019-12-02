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
        public double Weight { get; set; }
        public UInt16 Height { get; set; }
        public string Gender { get; set; }
        public DateTime WorkoutSince { get; set; }
        public List<Role> Roles { get; set; } = new List<Role>();
        public List<Serie> Series { get; set; } = new List<Serie>();
    }
}