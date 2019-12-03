using System.Collections.Generic;

namespace PocketGym.Domain.Core.Entities
{
    public class Target : IEntity
    {
        public string Id { get; set;  }
        public string Title { get; set; }
        public List<Serie> Series { get; set; } = new List<Serie>(7);
    }
}