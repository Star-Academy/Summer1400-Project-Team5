using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Talent.Models;
using System.Threading.Tasks;
using Talent.Services.Interfaces;

namespace Talent.Data.Entities
{
    public class Pipeline
    {
        [Key] public int PipelineId { get; set; }
        public string Name { get; set; }
        public DataSource Source { get; set; }
        public DataSource Destination { get; set; }
        public AppUser Owner { get; set; }
        public PipelineProcess Process { get; set; }

        public async Task Run(ProcessInfo processInfo, ISqlHandler sqlHandler)
        {
            await Process.Process(Source, processInfo, sqlHandler);
        }

        public void RunDemo(ISqlHandler sqlHandler)
        {
            Process.Process(Source, sqlHandler);
        }
    }
}