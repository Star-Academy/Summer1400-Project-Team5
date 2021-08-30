using System.Collections.Generic;

namespace Talent.Data.Entities
{
    public class PipelineProcess : Process
    {
        public List<Process> Processes { get; set; }
    }
}