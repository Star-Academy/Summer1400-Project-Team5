using System;
using System.Collections.Generic;
using Talent.Data.Entities;

namespace Talent.Models
{
    public class PipelineModel
    {
        public string Name { get; set; }
        public int SourceId { get; set; }
        public int DestinationId { get; set; }
        public int NumberOfProcesses { get; set; }
        
        public PipelineModel(string name, int sourceId, int destinationId)
        {
            Name = name;
            SourceId = sourceId;
            DestinationId = destinationId;
            NumberOfProcesses = 0;
        }
    }
}