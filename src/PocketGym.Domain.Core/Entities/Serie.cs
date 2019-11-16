using System.Collections.Generic;

namespace PocketGym.Domain.Core.Entities
{
    public class Serie : IEntity
    {
        public string Id { get; set;  }
        public string Title { get; set; }
        public int RestTimeBetweenExercisesInSeconds { get; set; }
        public List<Exercise> Exercises { get; set; } = new List<Exercise>();
    }
}