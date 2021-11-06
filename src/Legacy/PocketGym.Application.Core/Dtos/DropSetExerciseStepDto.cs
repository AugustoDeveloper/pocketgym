namespace PocketGym.Application.Core.Dtos
{
    public class DropSetExerciseStepDto : IExerciseStepDto
    {        
        public string Id { get; set; }
        public string Name { get; set; }
        public SingleExerciseStepDto[] Steps { get; set; }
        public int NumberOfRepetitions { get; set; }
        public int NumberOfExerciseRepetitions { get; set; }
        public int RestTimeInSeconds { get; set; }
    }
}