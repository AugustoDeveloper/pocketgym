using System.Collections.Generic;

namespace PocketGym.Domain.Core.Entities
{
    public class Serie : IEntity
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public long UserId { get; set; }
        public List<ExerciseSerie> ExercisesSeries { get; set; } = new List<ExerciseSerie>();
    }
}