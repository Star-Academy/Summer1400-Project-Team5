using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Talent.Data.Entities
{
    public class Pipeline
    {
        [Key] public int PipelineId { get; set; }

        public string Name { get; set; }

        public DataSource Source { get; set; }

        [ForeignKey("Source")] public int SourceId { get; set; }
        
        public DataSource Destination { get; set; }

        [ForeignKey("Destination")] public int DestinationId { get; set; }

        public AppUser Owner { get; set; }
        [ForeignKey("Owner")] public string OwnerId { get; set; }
        
        public PipelineProcess Process { get; set; }

        
    }
}