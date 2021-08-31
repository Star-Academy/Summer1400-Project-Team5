using System;
using System.Collections.Generic;
using Talent.Data.Entities;

namespace Talent.Models
{
    public class PipelineModel
    {
        public int PipelineId { get; set; }
        public string Name { get; set; }
        public int SourceId { get; set; }
        public int DestinationId { get; set; }
        public int NumberOfProcesses { get; set; }
        public List<Processor> Processors { get; set; }
        
        public PipelineModel(string name, int sourceId, int destinationId)
        {
            Name = name;
            SourceId = sourceId;
            DestinationId = destinationId;
            NumberOfProcesses = 0;
        }
        
        public PipelineModel() {}
    }
}