using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Reflection.Emit;
using System.Collections.Generic;
using Talent.Models;


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
        
        
        public DataSource Process(DataSource source,ProcessInfo processInfo)
        {
            DataSource result = source;
            foreach (var process in Processes)
            {
                result = process.Process(result);
                processInfo.CurrentProcessIndex = process.Index + 1;
            }
            return result;
        }
        
        public DataSource Process(DataSource source)
        {
            DataSource result = source;
            foreach (var process in Processes)
            {
                result = process.Process(result);
            }
            return result;
        }
    }
}