namespace PocketGym.Application.Core.Dtos
{
    public interface IExerciseStepDto : IDataTransferObject
    {
        string Id { get; set; }
        string Name { get; set; }
        int NumberOfRepetitions { get; set; }
        int NumberOfExerciseRepetitions { get; set; }
        int RestTimeInSeconds { get; set; }
    }
}