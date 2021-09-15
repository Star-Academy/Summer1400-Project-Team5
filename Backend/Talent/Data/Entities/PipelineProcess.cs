using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Talent.Services.Interfaces;
using System.Collections.Generic;
using Talent.Models;
using System.Threading.Tasks;

namespace Talent.Data.Entities
{
    public class PipelineProcess
    {
        [Key]
        public int PipelineProcessId { get; set; }
        public List<Processor> Processes { get; set; }

        [ForeignKey("Pipeline")]
        public int PipelineId { get; set; }
        public Pipeline Pipeline { get; set; }

        public Task<TempDataSource> Process(DataSource source, ProcessInfo processInfo, ISqlHandler sqlHandler)
        {
            return Task.Run(() =>
            {
                var tempDataSource = source.CloneTable(sqlHandler);
                foreach (var process in Processes)
                {
                    process.Process(tempDataSource);
                    processInfo.CurrentProcessIndex = process.Index + 1;
                }
                return tempDataSource;
            });
        }

        public TempDataSource Process(DataSource source, ISqlHandler sqlHandler)
        {
            var tempDataSource = source.CloneTable(sqlHandler);
            foreach (var process in Processes)
            {
                process.Process(tempDataSource);
            }
            return tempDataSource;
        }
    }
}