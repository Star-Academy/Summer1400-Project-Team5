using System.Threading.Tasks;
using System.Threading;
using System.Diagnostics;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Talent.Models;

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

        public void RunDemo()
        {
            var demo = Source.CloneDemo();
            Destination = Process.Process(demo); //ToDo overwrite
        }

        public async Task Run(ProcessInfo processInfo)
        {
            await Task.Run(() => 
            {
                var clone = Source.Clone();
                Destination = Process.Process(clone,processInfo); //ToDo overwrite handel exception
            }, processInfo.CancellationToken);
        }
    }
}