namespace PocketGym.Application.Core.Dtos
{
    public class SerieDto : IDataTransferObject
    {
        public long Id { get; set; }
        public long UserId { get; set; }
        public string Name { get; set; }
    }
}