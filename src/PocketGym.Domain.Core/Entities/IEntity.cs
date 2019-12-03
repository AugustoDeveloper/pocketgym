namespace PocketGym.Domain.Core.Entities
{
    public interface IEntity
    {
        string Id { get; set; }
    }

    public interface IEntity<TIdType>
    {
        TIdType Id { get; set; }
    }
}