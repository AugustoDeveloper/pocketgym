namespace PocketGym.Domain.Core.Entities
{
    public class User : IEntity
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public int Age { get; set; }
    }
}