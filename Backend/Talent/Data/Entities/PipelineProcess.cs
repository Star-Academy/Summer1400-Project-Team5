using System.ComponentModel.DataAnnotations;

namespace Talent.Data.Entities
{
    public class PipelineProcess : Process
    {
        [Key] public int Id { get; set; }
    }
}