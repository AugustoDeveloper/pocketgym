using System.Collections.Generic;

namespace PocketGym.Application.Core.Dtos
{
    public class ExerciseSerieDto : IDataTransferObject
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public List<ExerciseDto> Exercises { get; set; }
        public int NumberOfRepetitions { get; set; }
        public int RestTimeInSeconds { get; set; }
    }
}