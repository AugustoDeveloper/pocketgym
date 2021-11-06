using System;
using System.Collections.Generic;

namespace PocketGym.Domain.Core.Entities
{
    public class DropSetExerciseStep : IExerciseStep
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public ExerciseStepType Type => ExerciseStepType.DropSet;
        public SingleExerciseStep[] Steps { get; set; }
        public int NumberOfRepetitions { get; set; }
        public int NumberOfExerciseRepetitions { get; set; }
        public int RestTimeInSeconds { get; set; }
    }
}