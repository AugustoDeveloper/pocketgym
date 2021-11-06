using System;
using System.Collections.Generic;

namespace PocketGym.Domain.Core.Entities
{
    public class Serie : IEntity
    {
        public string Id { get; set;  }
        public string Title { get; set; }
        public int RestTimeBetweenExercisesInSeconds { get; set; }
        public DayOfWeek[] WorkingDays { get; set; }
        public List<IExerciseStep> Steps { get; set; } = new List<IExerciseStep>();
    }
}