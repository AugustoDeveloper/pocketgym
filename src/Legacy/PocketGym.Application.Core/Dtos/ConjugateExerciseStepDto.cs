namespace PocketGym.Application.Core.Dtos
{
    public class ConjugateExerciseStepDto : IExerciseStepDto
    {        
        public string Id { get; set; }
        public string Name { get; set; }
        public SingleExerciseStepDto FirstExercise { get; set; }
        public SingleExerciseStepDto SecondExercise { get; set; }
        public int NumberOfRepetitions { get; set; }
        public int NumberOfExerciseRepetitions { get; set; }
        public int RestTimeInSeconds { get; set; } 
    }
}