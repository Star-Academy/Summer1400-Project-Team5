using System;
using System.Threading.Tasks;
using Talent.Data.Entities;

namespace Talent.Services.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        public IGenericRepository<Aggregate> Aggregates { get; }
        public IGenericRepository<Aggregation> Aggregations { get; }
        public IGenericRepository<DataSource> DataSources { get; }
        public IGenericRepository<Filter> Filters { get; }
        public IGenericRepository<Join> Joins { get; }
        public IGenericRepository<Pipeline> Pipelines { get; }
        public IGenericRepository<PipelineProcess> PipelineProcesses { get; }
        public IGenericRepository<TempDataSource> TempDataSources { get; }
        public IGenericRepository<DataList> DataSourcesList { get; }

        public Task Save();
    }
}
