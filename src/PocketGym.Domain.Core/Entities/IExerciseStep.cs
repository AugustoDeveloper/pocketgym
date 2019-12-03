using System;
using System.Collections.Generic;

namespace PocketGym.Domain.Core.Entities
{
    public interface IExerciseStep : IEntity
    {
        string Name { get; set; }
        ExerciseStepType Type { get; }
        int NumberOfRepetitions { get; set; }
        int NumberOfExerciseRepetitions { get; set; }
        int RestTimeInSeconds { get; set; }
    }
}