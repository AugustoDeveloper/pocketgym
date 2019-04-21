using System;
namespace PocketGym.Application.Core.Dtos
{
    public class ExerciseDto : IDataTransferObject
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string MuscleGroup { get; set; }
    }
}