using Talent.Data;
using Talent.Data.Entities;
using Talent.Services.Interfaces;
using System;
using System.Threading.Tasks;

namespace Talent.Services.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private IGenericRepository<DataSource> _dataSources;
        private IGenericRepository<Pipeline> _pipelines;
        private IGenericRepository<PipelineProcess> _pipelineProcesses;
        private IGenericRepository<Processor> _processes;
        private IGenericRepository<TempDataSource> _tempDataSources;
        private IGenericRepository<GroupByColumn> _groupByColumns;
        
        private readonly AppDbContext _dbContext;
        
        public UnitOfWork(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public IGenericRepository<DataSource> DataSources => _dataSources ??= new GenericRepository<DataSource>(_dbContext);
        public IGenericRepository<Pipeline> Pipelines => _pipelines ??= new GenericRepository<Pipeline>(_dbContext);
        public IGenericRepository<Processor> Processes => _processes ??= new GenericRepository<Processor>(_dbContext);
        public IGenericRepository<PipelineProcess> PipelineProcesses => _pipelineProcesses ??= new GenericRepository<PipelineProcess>(_dbContext);

        public IGenericRepository<TempDataSource> TempDataSources => _tempDataSources ??= new GenericRepository<TempDataSource>(_dbContext);

        public IGenericRepository<GroupByColumn> GroupByColumns =>
            _groupByColumns ??= new GenericRepository<GroupByColumn>(_dbContext);


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
