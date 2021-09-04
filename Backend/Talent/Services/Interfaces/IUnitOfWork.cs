using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Talent.Data.Entities;
using Talent.Models.ProcessInfo;

namespace Talent.Services.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        public IGenericRepository<DataSource> DataSources { get; }
        public IGenericRepository<Pipeline> Pipelines { get; }
        public IGenericRepository<Processor> Processes { get; }
        public IGenericRepository<PipelineProcess> PipelineProcesses { get; }
        public IGenericRepository<TempDataSource> TempDataSources { get; }
        public Dictionary<int,ProcessInfo> ProcessInfos { get; }
        public IGenericRepository<GroupByColumn> GroupByColumns { get; }
        public Task Save();
    }
}
