using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Reflection.Emit;
using System.Collections.Generic;

namespace Talent.Data.Entities
{
    public class PipelineProcess : IProcessor
    {
        
        [Key]
        public int PipelineProcessId { get; set; }
        public List<Processor> Processes { get; set; }

        [ForeignKey("Pipeline")]
        public int PipelineId { get; set; }
        public Pipeline Pipeline { get; set; }
        public DataSource Process(DataSource source)
        {
            throw new System.NotImplementedException();
        }
        
        public override string ToString()
        {
            foreach (var processor in Processes)
            {
                processor.
            }
        }
    }
    
    
}