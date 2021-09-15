using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Talent.Models;
using Talent.Services.Interfaces;

namespace Talent.Data.Entities
{
    public abstract class Processor
    {
        [Key]
        public int ProcessId { get; set; }
        public int Index { get; set; }
        public ProcessType ProcessType { get; set; }

        [ForeignKey("PipelineProcess")]
        public int PipelineProcessId { get; set; }
        public PipelineProcess PipelineProcess { get; set; }

        public abstract TempDataSource Process(TempDataSource source, ISqlHandler sqlHandler);
    }
}