using System;
using System.Runtime.Serialization;
using Newtonsoft.Json;

namespace PocketGym.Application.Core.Dtos
{
    public class HealthDto : IDataTransferObject
    {
        public bool Success { get; set; }
        public string[] Details { get; set; }
    }
}