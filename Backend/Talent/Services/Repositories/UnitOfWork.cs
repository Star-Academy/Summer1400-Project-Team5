using Talent.Data;
using Talent.Data.Entities;
using Talent.Services.Interfaces;
using System;
using System.Threading.Tasks;

namespace Talent.Services.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private IGenericRepository<Aggregate> _aggregates;
        private IGenericRepository<Aggregation> _aggregations;
        private IGenericRepository<DataSource> _dataSources;
        private IGenericRepository<Filter> _filters;
        private IGenericRepository<Join> _joins;
        private IGenericRepository<Pipeline> _pipelines;
        private IGenericRepository<PipelineProcess> _pipelineProcesses;
        private IGenericRepository<TempDataSource> _tempDataSources;
        private IGenericRepository<DataList> _dataSourcesList;

        private readonly AppDbContext _dbContext;
        public UnitOfWork(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public IGenericRepository<Aggregate> Aggregates => _aggregates ??= new GenericRepository<Aggregate>(_dbContext);
        public IGenericRepository<Aggregation> Aggregations => _aggregations ??= new GenericRepository<Aggregation>(_dbContext);
        public IGenericRepository<DataSource> DataSources => _dataSources ??= new GenericRepository<DataSource>(_dbContext);
        public IGenericRepository<Filter> Filters => _filters ??= new GenericRepository<Filter>(_dbContext);
        public IGenericRepository<Join> Joins => _joins ??= new GenericRepository<Join>(_dbContext);
        public IGenericRepository<Pipeline> Pipelines => _pipelines ??= new GenericRepository<Pipeline>(_dbContext);
        public IGenericRepository<PipelineProcess> PipelineProcesses => _pipelineProcesses ??= new GenericRepository<PipelineProcess>(_dbContext);

        public IGenericRepository<TempDataSource> TempDataSources => _tempDataSources ??= new GenericRepository<TempDataSource>(_dbContext);

        public IGenericRepository<DataList> DataSourcesList => _dataSourcesList ??= new GenericRepository<DataList>(_dbContext);

        public void Dispose()
        {
            _dbContext.Dispose();
            GC.SuppressFinalize(this);
        }

        public async Task Save()
        {
            await _dbContext.SaveChangesAsync();
        }
    }
}
