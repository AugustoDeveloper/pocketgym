using System;
namespace PocketGym.Application.Core.Dtos
{
    public class ExerciseDto : IDataTransferObject
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string MuscleGroup { get; set; }
        public int NumberOfRepetitions { get; set; }
        public int NumberOfExerciseRepetitions { get; set; }
        public int RestTimeInSeconds { get; set; }
    }
}