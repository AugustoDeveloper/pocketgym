namespace PocketGym.Application.Core.Dtos
{
    public class SingleExerciseStepDto : IExerciseStepDto
    {        
        public string Id { get; set; }
        public string Name { get; set; }
        public int NumberOfRepetitions { get; set; }
        public int NumberOfExerciseRepetitions { get; set; }
        public int RestTimeInSeconds { get; set; }
        public double Weight { get; set; }
        public int ExerciseTimeInSeconds { get; set; }
        public string ExerciseId { get; set; }   
    }
}