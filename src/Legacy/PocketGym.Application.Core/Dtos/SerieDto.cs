using System;

namespace PocketGym.Application.Core.Dtos
{
    public class SerieDto : IDataTransferObject
    {
        public string Id { get; set; }
        public string Title { get; set; }
        public int RestTimeBetweenExercisesInSeconds { get; set; }
        public DayOfWeek[] WorkingDays { get; set; }
    }
}