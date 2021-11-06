using System;
using System.Collections.Generic;

namespace PocketGym.Domain.Core.Entities
{
    public class SingleExerciseStep : IExerciseStep
    {

        public string Id { get; set; }
        public string Name { get; set; }
        public ExerciseStepType Type => ExerciseStepType.SingleExercise;
        public int NumberOfRepetitions { get; set; }
        public int NumberOfExerciseRepetitions { get; set; }
        public int RestTimeInSeconds { get; set; }
        public string ExerciseId { get; set; }
        public double Weight { get; set; }
        public int ExerciseTimeInSeconds { get; set; }   
    }
}