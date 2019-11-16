using System;
using System.Collections.Generic;

namespace PocketGym.Domain.Core.Entities
{
    public class ExerciseSerie : IEntity
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public List<Exercise> Exercises { get; set; } = new List<Exercise>();
        public int NumberOfRepetitions { get; set; }
        public int RestTimeInSeconds { get; set; }
    }
}