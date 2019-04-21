namespace PocketGym.Domain.Core.Entities
{
    public class Exercise : IEntity
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string MuscleGroup { get; set; }
    }
}