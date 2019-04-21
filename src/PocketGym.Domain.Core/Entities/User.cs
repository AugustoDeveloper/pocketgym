using System.Collections.Generic;

namespace PocketGym.Domain.Core.Entities
{
    public class User : IEntity
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string Username { get; set; }
        public byte[] PasswordHash { get; set; }
        public byte[] PasswordSalt { get; set; }
        public int Age { get; set; }
        public Role Role { get; set; }
    }
}